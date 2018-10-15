using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Common.ViewModels;

namespace Common.Overlay
{
    public enum State
    {
        VISIBLE,
        HIDDEN
    }


    public class Overlay : PropertyChangedBase, IOverlay
    {
        private IOverlayToken _token;
        public IOverlayToken Token
        {
            get { return _token; }
        }


        public bool IsActive
        {
            get { return _token != null; }
        }


        public IOverlayToken DisplayOverlay(IOverlayToken overlayToken)
        {
            overlayToken.OnOverlayHide += (s) => {
                if (s != Token) { // only current token can disable overlay
                    return;
                }
                _token = null;
                NotifyOfPropertyChange(() => Token);
                NotifyOfPropertyChange(() => IsActive);
            };

            _token = overlayToken;
            NotifyOfPropertyChange(() => Token);
            NotifyOfPropertyChange(() => IsActive);

            ScreenExtensions.TryActivate(overlayToken.Content);

            return overlayToken;
        }


        public IOverlayToken DisplayOverlay<VM>(VM content, bool isMandatory = false) where VM : IViewModel
        {
            IOverlayToken token = new OverlayToken(content, isMandatory);
            token.OnOverlayHide += (s) => {
                if (s != Token) { // only current token can disable overlay
                    return;
                }
                _token = null;
                NotifyOfPropertyChange(() => Token);
                NotifyOfPropertyChange(() => IsActive);
            };           

            _token = token;
            NotifyOfPropertyChange(() => Token);
            NotifyOfPropertyChange(() => IsActive);

            ScreenExtensions.TryActivate(content);

            return token;
        }


        public void HideOverlay()
        {
            if (Token != null) Token.HideOverlay();
        }
    }
}
