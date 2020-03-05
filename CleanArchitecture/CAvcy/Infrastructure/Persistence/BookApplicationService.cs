using Domain;
using Domain.Repository;
using Framework;
using Infrastructure.Commands.V1.BookReaders;
using Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class BookApplicationService : IApplicationService
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
                IDateService dateService)
        {
            _bookRepository = bookRepository;
            _readerRepository = readerRepository;
            _lendRepository = lendsRepository;
            _unitOfWork = unitOfWork;
            _dateService = dateService;
        }

        public Task Handle(object command)
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

            try
            {
                //_lendRepository.Update(bookLend);
                _bookRepository.Update(bookToBeReturned);
                await _unitOfWork.Commit();
            }
            catch(Exception e)
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
                    throw new ArgumentException("There was a problem creating the book reader");
            }

            try
            {
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
                    throw new ArgumentException("the reader doesn't exist");
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
                BookLend lastLend = bookToBeLent.LastLentWithNoReturn();
                await _lendRepository.Add(lastLend);
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
                    throw new ArgumentException("There was a problem creating the book");
            }

            try
            {
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
