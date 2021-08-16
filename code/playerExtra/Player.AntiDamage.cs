using Sandbox;
using System;
partial class SandboxPlayer
{
	[Net] public bool AntiDamage { get; set; }

	public void AntiDamageInit()
	{
		AntiDamage = true;
	}

}