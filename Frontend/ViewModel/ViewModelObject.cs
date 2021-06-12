﻿using IntroSE.Kanban.Frontend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Frontend.ViewModel
{
    class ViewModelObject :NotifiableObject
    {
        protected BackendController backendController;

        public ViewModelObject()
        {
            this.backendController = new BackendController();
        }
    }
}
