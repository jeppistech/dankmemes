// Copyright 2001-2016 Crytek GmbH / Crytek Group. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using CryEngine.Common;
using CryEngine.Attributes;

namespace CryEngine.EntitySystem
{
	/*public sealed class EntityClassRegistry
	{
		#region HELPER CLASSES
		internal class CreationHelper : GameObjectExtensionCreatorHelper
		{
			private Type _t;

			public CreationHelper(Type t)
			{
				if(!typeof(IGameObjectExtension).IsAssignableFrom(t))
					throw new ArgumentException(String.Format("Type information of '{0}' is invalid! Must inherit from IGameOjbectExtension (or BaseEntityClass)!", t.Name));
				_t = t;
			}

			public override IGameObjectExtension Instantiate ()
			{
				return (IGameObjectExtension)Activator.CreateInstance (_t);
			}
		}
		#endregion

		#region Fields
		private static readonly Dictionary<Type, GameObjectExtensionCreatorBase> s_creators = new Dictionary<Type, GameObjectExtensionCreatorBase>();
		private static readonly Dictionary<Type, Func<object, ScriptAnyValue>> s_typeTranslator = new Dictionary<Type, Func<object, ScriptAnyValue>>
		{
			{ typeof(int), obj => new ScriptAnyValue((int)obj) },
			{ typeof(float), obj => new ScriptAnyValue((float)obj) },
			{ typeof(string), obj => new ScriptAnyValue{ type = ScriptAnyType.ANY_TSTRING, str = (string)obj } },
			{ typeof(Vec3), obj => new ScriptAnyValue((Vec3)obj) },
			{ typeof(Ang3), obj => new ScriptAnyValue((Ang3)obj) },
			{ typeof(double), obj => new ScriptAnyValue(Convert.ToSingle(obj)) },
			{ typeof(bool), obj => new ScriptAnyValue((bool)obj) }
		};
		#endregion

		#region Constructors
		internal EntityClassRegistry()
		{
		}
		#endregion

		#region Methods
		public void RegisterEntityClass<T>() where T : BaseEntityClass
		{
			RegisterEntityClass (typeof(T));
		}

		private void AddProperty(IScriptTable table, PropertyInfo pi)
		{
			Log.Always ("Add Property: {0}", pi.Name);
			if (pi.GetCustomAttributes (typeof(HideFromInspectorAttribute), true).Any ())
				return;

			Log.Always("  jopp");

			DefaultValueAttribute attrDefault = (DefaultValueAttribute)pi.GetCustomAttributes (typeof(DefaultValueAttribute), true).FirstOrDefault ();
			object defaultValue = attrDefault != null ? attrDefault.Value : (pi.PropertyType.IsValueType ? Activator.CreateInstance (pi.PropertyType) : null);

			if (s_typeTranslator.ContainsKey (pi.PropertyType))
			{
				table.SetValueAny (pi.Name, s_typeTranslator [pi.PropertyType] (defaultValue));
				Log.Always("  aaand added");
			}
			else
			{
				Log.Always("  oooops, is sub");
				IScriptTable subTable = Global.gEnv.pScriptSystem.CreateTable ();
				subTable.AddRef ();
				foreach (PropertyInfo subPi in pi.PropertyType.GetProperties())
					AddProperty (subTable, subPi);
				table.SetValueAny (pi.Name, new ScriptAnyValue (subTable));
			}
		}

		public void RegisterEntityClass(Type t)
		{
			if (t.GetCustomAttributes (typeof(HideFromInspectorAttribute), true).Any ())
			{
				Log.Warning("EntityClass '{0}' marked to ignore. Not registering!", t.Name);
				return;
			}

			EntityClassAttribute attr = (EntityClassAttribute)t.GetCustomAttributes(typeof(EntityClassAttribute), true).FirstOrDefault();	
			IEntityClassRegistry.SEntityClassDesc desc = new IEntityClassRegistry.SEntityClassDesc ();

			string name = t.Name;
			if (attr == null || String.IsNullOrEmpty (attr.Name))
				Log.Warning ("EntityClass '{0}' has no valid name. Using type name", t.Name);
			else
				name = attr.Name;
			desc.sName = name;

			BaseScriptProperties props = new BaseScriptProperties ();
			props.CreateTables ();
			Global.gEnv.pScriptSystem.SetGlobalAny (name, new ScriptAnyValue (props.EntityTable));

			// TODO: flow node factory stuff!!!!

			// TODO: Register Properties
			foreach (PropertyInfo pi in t.GetProperties())
				AddProperty (props.PropertiesTable, pi);

			if (attr != null && !String.IsNullOrEmpty (attr.EditorPath))
				props.EditorTable.SetValueAny ("EditorPath", new ScriptAnyValue{ type = ScriptAnyType.ANY_TSTRING, str = attr.EditorPath });

			props.EntityTable.SetValueAny ("Editor", new ScriptAnyValue(props.EditorTable));
			props.EntityTable.SetValueAny ("Properties", new ScriptAnyValue(props.PropertiesTable));

			//TODO: if fucking magic regarding InstanceProperties
			//	props.EntityTable.SetValueAny("PropertiesInstance", new ScriptAnyValue(props.InstancePropertiesTable));

			string nodeName = String.Format ("entity:{0}", name);
			// close node factory
			// insert node factory . .somewhere

			desc.pScriptTable = props.EntityTable;

			if (!s_creators.ContainsKey (t))
				s_creators.Add (t, new GameObjectExtensionCreatorBase (new CreationHelper(t)));
			GameObjectExtensionCreatorBase creator = s_creators [t];

			Log.Always ("Register entity class: {0}", name);
			if (attr != null && !String.IsNullOrEmpty (attr.EditorPath))
				Log.Always ("  EditorPath: {0}", attr.EditorPath);

			Global.gEnv.pGame.GetIGameFramework ().GetIGameObjectSystem ().RegisterExtension (name, creator, desc);
			if (attr != null && attr.Hide)
			{
				IEntityClass entClass = Global.gEnv.pEntitySystem.GetClassRegistry ().FindClass (name);
				entClass.SetFlags (entClass.GetFlags () | (uint)EEntityClassFlags.ECLF_INVISIBLE);
			}
		}

		public void RegisterAll()
		{
			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
			{
				Type tEntityClass = typeof(BaseEntityClass);
				foreach (Type t in asm.GetTypes()) 
				{
					if (t == tEntityClass || !tEntityClass.IsAssignableFrom (t))
						continue;

					RegisterEntityClass (t);
				}
			}
		}

		public void UnregisterAll()
		{
			throw new NotImplementedException ("AAAAAAAAAAAAHHHHHHHHHHHHH!!!!!!!!!!!!!!!!!!!!");
		}
		#endregion
	}*/
}

