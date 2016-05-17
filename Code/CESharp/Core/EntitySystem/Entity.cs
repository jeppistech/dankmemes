// Copyright 2001-2016 Crytek GmbH / Crytek Group. All rights reserved.

using CryEngine.Common;

namespace CryEngine.EntitySystem
{
	public class Entity
	{
		public uint ID { get; private set; }
		public Vec3 Position { get { return BaseEntity.GetPos (); } set { BaseEntity.SetPos (value); } }
		public Quat Rotation { get { return BaseEntity.GetRotation(); } set{ BaseEntity.SetRotation (value);} }
		public Vec3 Scale { get { return BaseEntity.GetScale(); } set{ BaseEntity.SetScale (value);} }
		public string Name { get { return BaseEntity.GetName (); } }
		public IMaterial Material { get { return BaseEntity.GetMaterial(); } }
		public bool Exists { get { return BaseEntity != null; } }

		public IEntity BaseEntity
		{
			get 
			{
				if (ID == 0)
					return null;
				return Env.EntitySystem.GetEntity(ID);
			}
		}

		public Entity(IEntity baseEntity)
		{
			ID = baseEntity.GetId();
		}

		public static Entity ById(uint id)
		{
			if (id == 0)
				return null;
			return new Entity(Env.EntitySystem.GetEntity(id));
		}

		public static Entity ByName(string name)
		{
			var baseEnt = Env.EntitySystem.FindEntityByName (name);
			return baseEnt != null ? new Entity(baseEnt) : null;
		}

		public static Entity Instantiate(Vec3 position, Quat rot, float scale, string model, string material = null)
		{
			SEntitySpawnParams spawnParams = new SEntitySpawnParams() 
			{
				pClass = Env.EntitySystem.GetClassRegistry().GetDefaultClass(),
				vPosition = position,
				vScale = new Vec3(scale)
			};

			var spawnedEntity = Env.EntitySystem.SpawnEntity(spawnParams, true);
			spawnedEntity.LoadGeometry(0, model);
			spawnedEntity.SetRotation(rot);

			if (material != null)
			{
				IMaterial mat = Global.gEnv.p3DEngine.GetMaterialManager().LoadMaterial(material);
				spawnedEntity.SetMaterial(mat);
			}

			var physics = new SEntityPhysicalizeParams() 
			{
				density = 1,
				mass = 0f,
				type = (int)EPhysicalizationType.ePT_Rigid,
			};
			spawnedEntity.Physicalize(physics);
			return new Entity(spawnedEntity);
		}

		public void Remove()
		{
			Env.EntitySystem.RemoveEntity (ID);
		}
	}
}
