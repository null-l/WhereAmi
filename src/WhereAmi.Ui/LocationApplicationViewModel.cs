using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reactive.Concurrency;
using System.Runtime.CompilerServices;
using Reactive.Bindings;

namespace WhereAmi
{


    internal class LocationApplicationViewModel 
    {

        public LocationApplicationViewModel(  )
        {
            ListenCommand = new ReactiveCommand<bool>(Scheduler.Immediate);
            CloseCommand = new ReactiveCommand(Scheduler.Default);
        }

        public ReactiveCommand<bool> ListenCommand { get; }

        public ReactiveCommand CloseCommand { get; }
        
    }

}