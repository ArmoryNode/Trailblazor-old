using Microsoft.AspNetCore.Components;

namespace Trailblazor.Client.Shared.Components
{
    public abstract class BaseComponent : ComponentBase
    {
        protected readonly Guid _componentId = Guid.NewGuid();

        protected event EventHandler<bool>? EnabledChanged;
        private bool _enabled = true;
        [Parameter]
        public bool Enabled
        {
            get => _enabled;
            set
            {
                EnabledChanged?.Invoke(this, value);
                _enabled = value;
            }
        }

        protected event EventHandler<bool>? VisibleChanged;
        private bool _visible = true;
        [Parameter]
        public bool Visible {
            get => _visible;
            set
            {
                VisibleChanged?.Invoke(this, value);
                _visible = value;
            }
        }
    }
}
