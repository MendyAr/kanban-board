﻿using Presentation.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace IntroSE.Kanban.Frontend.Model
{ 
    class Board
    {
        ObservableCollection<Task> tasks;
        public string CreatorEmail { get; private set; }

        public string CreatorFormat { get => "Created by" + CreatorEmail; }
        
    }
}
