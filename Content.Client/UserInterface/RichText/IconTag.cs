using System.Diagnostics.CodeAnalysis;
using Content.Shared.StatusIcon;
using Content.Shared.CCVar; // Starlight
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.RichText;
using Robust.Shared.Prototypes;
using Robust.Shared.Utility;

namespace Content.Client.UserInterface.RichText;

public sealed partial class IconTag : IMarkupTag
{
    [Dependency] private IPrototypeManager _prototype = default!;
    [Dependency] private IEntitySystemManager _entitySystem = default!;
    private SpriteSystem? _spriteSystem;

    public string Name => "icon";

    public bool TryGetControl(MarkupNode node, [NotNullWhen(true)] out Control? control)
    {
        if (!node.Attributes.TryGetValue("src", out var id) || id.StringValue == null)
        {
            control = null;
            return false;
        }
        _spriteSystem ??= _entitySystem.GetEntitySystem<SpriteSystem>();
        TextureRect? icon = null;

    _prototype.TryIndex<JobIconPrototype>(id.StringValue, out var jobProto);

        if (jobProto != null)
        {
            var spec = jobProto.Icon;

                var texture = _spriteSystem.Frame0(spec);
                icon = new TextureRect
                {
                    Texture = texture,
                    SetWidth = 19,
                    SetHeight = 19,
                    Stretch = TextureRect.StretchMode.Scale,
                    Margin = new Thickness(0, 3, 0, 4),
                    VerticalAlignment = TextureRect.VAlignment.Center,
                    HorizontalAlignment = TextureRect.HAlignment.Center,
                    MouseFilter = Control.MouseFilterMode.Stop,
                };

        }
        if (node.Attributes.TryGetValue("tooltip", out var tooltip) && tooltip.StringValue != null)
        {
            icon?.ToolTip = tooltip.StringValue;
        }


        return (control = icon) != null;
    }
}
