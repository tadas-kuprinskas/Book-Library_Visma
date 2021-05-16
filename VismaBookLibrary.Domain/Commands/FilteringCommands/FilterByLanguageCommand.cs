﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VismaBookLibrary.Domain.Interfaces;
using VismaBookLibrary.Domain.Models;

namespace VismaBookLibrary.Domain.Commands.FilteringCommands
{
    public class FilterByLanguageCommand : IFilterCommand
    {
        private readonly IFileService _fileService;
        private readonly IValidationService _validationService;

        public FilterByLanguageCommand(IFileService fileService, IValidationService validationService)
        {
            _fileService = fileService;
            _validationService = validationService;
        }

        public void Execute(string option, string phrase)
        {
            var filteredTaken = _fileService.GetAll().Where(b => b.TakenBy != null)
                .Where(b => b.Language.Contains(phrase, StringComparison.OrdinalIgnoreCase));

            var filteredAvailable = _fileService.GetAll().Where(b => b.TakenBy == null)
                .Where(b => b.Language.Contains(phrase, StringComparison.OrdinalIgnoreCase));

            var filteredAll = _fileService.GetAll().Where(b => b.Language.Contains(phrase, StringComparison.OrdinalIgnoreCase));

            _validationService.ValidateFilterOption(filteredAvailable, filteredTaken, filteredAll, option);
        }
    }
}
