using Domain;
using Domain.Repository;
using Domain.ValueObjects;
using Framework;
using Framework.ValueObjects;
using Infrastructure.Commands.V1.BookReaders;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class BookApplicationService : ApplicationService
    {
        private readonly IBooksRepository _bookRepository;
        private readonly IBookReadersRepository _readerRepository;
        private readonly IBookLendsRepository _lendRepository;
        private readonly IUnitOfWork _unitOfWork;

        private readonly IDateService _dateService;

        public BookApplicationService(
            IBooksRepository bookRepository,
            IBookReadersRepository readerRepository,
            IBookLendsRepository lendsRepository,
                IUnitOfWork unitOfWork,
                IStoreAggregate<BookGuid> bookstoreAggregate,
                IStoreAggregate<BookReaderGuid> readerstoreAggregate,
                IDateService dateService)
        {
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
            _lendRepository = lendsRepository;
            _unitOfWork = unitOfWork;
            _bookstoreAggregate = bookstoreAggregate;
            _readerstoreAggregate = readerstoreAggregate;
            _dateService = dateService;
        }

        public override Task Handle(object command)
        {
            switch(command)
            {
                case Infrastructure.Commands.V1.Books.CreateBook e:
                    return HandleCreateBook(e);
                case Infrastructure.Commands.V1.Books.LendBook e:
                    return HandleLendBook(e);
                case Infrastructure.Commands.V1.Books.ReturnBook e:
                    return HandleReturnBook(e);
                case Infrastructure.Commands.V1.BookReaders.CreateReader e:
                    return HandleCreateReader(e);
            }
            return Task.CompletedTask;
        }

        private async Task HandleReturnBook(Commands.V1.Books.ReturnBook c)
        {
            Book bookToBeReturned = null;
            try
            {
                bookToBeReturned = await _bookRepository.Load(new Domain.ValueObjects.BookGuid(c.BookId));
            }
            catch (Exception e)
            {
                if (bookToBeReturned == null)
                    throw new ArgumentException("The book to be returned doesn't exist", e);
            }

            try
            {                
                bookToBeReturned.LoadLends(_lendRepository);
                c.DateReturned = _dateService.Now(); 
                bookToBeReturned.ReturnBook(c.DateReturned);
            }
            catch (Exception e)
            {
                throw new ArgumentException("There was no lend to return", e);
            }

            MetaEvent lastEvent = await base.SaveAggregateInEventStore(bookToBeReturned);

            try
            {
                bookToBeReturned.Version = VersionNumber.FromLong(lastEvent.Version);
                _bookRepository.Update(bookToBeReturned);
                await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new ArgumentException("There was a problem saving the information", e);
            }
        }

        private async Task HandleCreateReader(CreateReader c)
        {
            BookReader newBookReader = null;
            try
            {
                newBookReader =
                new BookReader(c.Name);
            }
            catch (Exception e)
            {
                if (newBookReader == null)
                    throw new ArgumentException("There was a problem creating the book reader", e);
            }

            MetaEvent lastEvent = await base.SaveAggregateInEventStore(newBookReader);

            try
            {
                newBookReader.Version = VersionNumber.FromLong(lastEvent.Version);
                await _readerRepository.Add(newBookReader);
                await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw new PersistenceException("Could not save the new book reader", e);
            }
        }

        private async Task HandleLendBook(Infrastructure.Commands.V1.Books.LendBook c)
        {
            Book bookToBeLent = null;
            try
            {
                bookToBeLent = await _bookRepository.Load(new Domain.ValueObjects.BookGuid(c.BookId));
            }
            catch (Exception e)
            {
                if (bookToBeLent == null)
                    throw new ArgumentException("The book to be lend doesn't exist", e);
            }

            BookReader theReader = null;
            try
            {
                theReader = await _readerRepository.Load(c.BookReaderId);
            }
            catch (Exception e)
            {
                if (theReader == null)
                    throw new ArgumentException("the reader doesn't exist", e);
            }

            // Getting the date directly from service
            DateTime dateLent = _dateService.Now();

            try
            {
                bookToBeLent.LendBook(theReader, dateLent);
            } 
            catch(Exception e)
            {
                throw e;
            }

            try
            {
                MetaEvent lastEvent = await base.SaveAggregateInEventStore(bookToBeLent);
                bookToBeLent.Version = VersionNumber.FromLong(lastEvent.Version);

                BookLend lastLend = bookToBeLent.LastLentWithNoReturn();
                await _lendRepository.Add(lastLend);
                _bookRepository.Update(bookToBeLent);// Add(lastLend);
                await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                throw e ;
            }
        }

        private async Task HandleCreateBook(Infrastructure.Commands.V1.Books.CreateBook c)
        {
            Book newBook = null;
            try
            {
                newBook =
                new Book(c.Title);
            }
            catch (Exception e)
            {
                if (newBook == null)
                    throw new ArgumentException("There was a problem creating the book", e);
            }

            try
            {
                MetaEvent lastEvent = await base.SaveAggregateInEventStore(newBook);
                newBook.Version = VersionNumber.FromLong(lastEvent.Version);
                await _bookRepository.Add(newBook);
                await _unitOfWork.Commit();
                
            }
            catch (Exception e)
            {
                throw new PersistenceException("Could not save the new book", e);
            }
        }
    }
}
