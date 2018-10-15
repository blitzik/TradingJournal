using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Overlay
{
    public interface IOverlayToken
    {
        IViewModel Content { get; }
        bool IsMandatory { get; }

        void HideOverlay();

        event Action<IOverlayToken> OnOverlayHide;
    }
}
