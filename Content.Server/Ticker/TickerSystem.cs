using Robust.Server.Player;
using Robust.Shared.Enums;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Map;
using Robust.Shared.Maths;

namespace Content.Server.Ticker;

public class TickerSystem : EntitySystem
{
    [Dependency] private readonly IPlayerManager _playerManager = default!;
    public MapId CurrentMapId;

    public override void Initialize()
    {
        base.Initialize();
        
        _playerManager.PlayerStatusChanged += OnPlayerStatusChanged;
    }

    private void OnPlayerStatusChanged(object? sender, SessionStatusEventArgs e)
    {
        if(e.NewStatus != SessionStatus.Connected) return;
        
        var uid = EntityManager.SpawnEntity("player", new MapCoordinates(Vector2.Zero, CurrentMapId));
        e.Session.AttachToEntity(uid);
        e.Session.JoinGame();
    }
}