// Copyright 2001-2016 Crytek GmbH / Crytek Group. All rights reserved.

using CryEngine.Common;

namespace CryEngine.EntitySystem
{
	/*public static class EntityFramework
	{
		#region Fields
		private static EntitySystemSink s_entitySystemSink;
		private static EntityClassRegistry s_entityClassRegistry;
		#endregion

		#region Properties
		public static EntityClassRegistry ClassRegistry { get { return s_entityClassRegistry; } }
		#endregion

		#region Methods
		public static void Instantiate()
		{
			s_entityClassRegistry = new EntityClassRegistry ();
			s_entitySystemSink = new EntitySystemSink ();
			s_entityClassRegistry.RegisterAll ();
			Global.gEnv.pEntitySystem.AddSink (s_entitySystemSink, (uint)IEntitySystem.SinkEventSubscriptions.AllSinkEvents, 0);
		}

		public static void Destroy()
		{
			Global.gEnv.pEntitySystem.RemoveSink (s_entitySystemSink);
			s_entitySystemSink.Dispose ();
			s_entitySystemSink = null;
			//s_entityClassRegistry.UnregisterAll ();
			s_entityClassRegistry = null;
		}
		#endregion
	}*/
}

