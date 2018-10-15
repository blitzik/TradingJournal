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


        private bool _isMandatory;
        public bool IsMandatory
        {
            get { return _isMandatory; }
        }


        public event Action<IOverlayToken> OnOverlayHide;

        public OverlayToken(IViewModel content, bool isMandatory = false)
        {
            _content = content;
            _isMandatory = isMandatory;
        }


        public void HideOverlay()
        {
            OnOverlayHide?.Invoke(this);
        }
    }
}
