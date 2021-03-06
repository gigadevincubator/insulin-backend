﻿using System;
using insulin_backend.Database.Models;

namespace insulin_backend.Database.Repository.Extended
{
    public class StepRepository : Repository<Step>, IStepRepository
    {
        private readonly DataContext _dataContext;
        public StepRepository(DataContext context) : base(context)
        {
            _dataContext = context;
        }

        public Step CreateStep(Step step)
        {
            try
            {
                _dataContext.Steps.Add(step);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return step;
        }

        public DataContext DataContext
        {
            get { return Context as DataContext;  }
        }
    }
}