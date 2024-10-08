using Sandbox.Entity;
using Sandbox.GameSystems.Player;

/// <summary>
/// Represents a Food entity that players can interact with to collect Food.
/// Inherits from BaseEntity for shared functionality.
/// </summary>
[Library( "Food", Title = "Food" )]
public sealed class Food : BaseEntity
{
	/// <summary>
	/// Amount of Food this entity represents.
	/// </summary>
	[Property, Sync] public int Amount { get; private set; } = 100;

	/// <summary>
	/// Called when the component is first created and added to a GameObject.
	/// Initializes the component and sets up the model and entity name.
	/// </summary>
	protected override void OnStart()
	{
		EntityName = "Food";
		base.OnStart();
	}

	/// <summary>
	/// Handles interaction when the player uses the default interaction key (e.g., "E").
	/// This method adds Food to the player's stats and destroys the Food entity.
	/// </summary>
	public override void InteractUse( SceneTraceResult tr, GameObject player )
	{
		Log.Info( "Interacting with food" );

		var playerStats = player.Components.Get<Player>();
		if ( playerStats != null )
		{
			playerStats.UpdateHunger( Amount );
			Sound.Play( "", Transform.World.Position ); // TODO: sound of eating
			DestroyFood();
		}
	}

	/// <summary>
	/// Destroys the food entity after it has been collected.
	/// </summary>
	[Broadcast]
	public void DestroyFood()
	{
		this.GameObject.Destroy();
	}
}
