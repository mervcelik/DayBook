using System;
using System.Collections.Generic;
using System.Text;

namespace Screens
{
    public class ViewModel
    {
        private double selectedValue;

        public double SelectedValue
        {
            get
            {
                return selectedValue;
            }
            set
            {
                selectedValue = value;
            }
        }
    }
}
