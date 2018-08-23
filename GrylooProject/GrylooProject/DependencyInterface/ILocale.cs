using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrylooProject.DependencyInterface
{
    public interface ILocale
    {
        string GetCurrent();

        void SetLocale();
    }
}
