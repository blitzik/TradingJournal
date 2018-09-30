using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModels;

namespace Common.Overlay
{
    public class OverlayToken : IOverlayToken
    {
        private IViewModel _content;
        public IViewModel Content
        {
            get { return _content; }
        }


        public event Action<IOverlayToken> OnOverlayHide;

        public OverlayToken(IViewModel content)
        {
            _content = content;
        }


        public void HideOverlay()
        {
            OnOverlayHide?.Invoke(this);
        }
    }
}
