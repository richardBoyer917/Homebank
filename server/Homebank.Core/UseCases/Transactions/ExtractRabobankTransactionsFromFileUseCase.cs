﻿using Homebank.Core.Converters;
using Homebank.Core.Domain.Entities;
using Homebank.Core.Dto.Transactions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Homebank.Core.UseCases.Transactions
{
    public class ExtractRabobankTransactionsFromFileUseCase : IRequestHandler<RabobankTransactionFileRequest, TransactionExtractionResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRabobankCsvConverter _csvConverter;

        public ExtractRabobankTransactionsFromFileUseCase(IUnitOfWork unitOfWork, IRabobankCsvConverter csvConverter)
        {
            _unitOfWork = unitOfWork;
            _csvConverter = csvConverter;
        }

        public async Task<TransactionExtractionResponse> Handle(RabobankTransactionFileRequest request, CancellationToken cancellationToken)
        {
            var transactionsFromFile = _csvConverter.Convert(request.TransactionFile).ToList();
            var transactionsFromDatabase = await _unitOfWork.Transactions.GetAll();
            var transactionsToCreate = new List<Transaction>();
            var transactionsThatAlreadyExist = new List<Transaction>();

            SplitTransactionsInto(transactionsToCreate, transactionsThatAlreadyExist, transactionsFromFile, transactionsFromDatabase);

            await _unitOfWork.Transactions.CreateMultiple(transactionsToCreate);

            return new TransactionExtractionResponse
            {
                NewTransactions = transactionsToCreate.Count,
                DuplicateTransactions = transactionsThatAlreadyExist.Count
            };
        }

        private static void SplitTransactionsInto(List<Transaction> transactionsToCreate, List<Transaction> transactionsThatAlreadyExist, List<Transaction> transactionsFromFile, IEnumerable<Transaction> transactionsFromDatabase)
        {
            foreach (var transactionFromFile in transactionsFromFile)
            {
                if (transactionsFromDatabase.Any(transactionFromDatabase => transactionFromDatabase.Equals(transactionFromFile)))
                {
                    transactionsThatAlreadyExist.Add(transactionFromFile);
                }
                else
                {
                    transactionsToCreate.Add(transactionFromFile);
                }
            }
        }
    }
}
