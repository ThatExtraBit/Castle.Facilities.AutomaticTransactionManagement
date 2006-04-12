// Copyright 2004-2006 Castle Project - http://www.castleproject.org/
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace Castle.ActiveRecord.Tests.Model.CompositeModel
{
	using System;
	using System.Collections;

	using NHibernate;
	
	using Castle.ActiveRecord.Framework;
	using NHibernate.Type;

	[ActiveRecord("Groups")]
	public class Group : ActiveRecordBase
	{
		private int _id;
		private String _name;
		private IList _agents;
		private IList _groups;
		private bool onSaveCalled, onUpdateCalled, onDeleteCalled, onLoadCalled;

		public Group()
		{
			_agents = new ArrayList();
			_groups = new ArrayList();
		}

		[PrimaryKey(PrimaryKeyType.Native)]
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		[Property]
		public String Name
		{
			get { return _name; }
			set { _name = value; }
		}

		[HasAndBelongsToMany(typeof (Agent),
			 Table = "GroupAgents",
			 CompositeKeyColumnRefs = new String[] {"OrgId", "Name"},
			 ColumnKey = "GroupId",
			 Lazy = true,
			 Inverse=true,
			 Cascade = ManyRelationCascadeEnum.SaveUpdate)]
		public IList Agents
		{
			get { return _agents; }
			set { _agents = value; }
		}
		
		[HasAndBelongsToMany(typeof (Group),
			 Table = "GroupOrgs",
			 ColumnRef = "GroupId",
			 ColumnKey = "OrgId",
			 Lazy = true,
			 Inverse=true,
			 Cascade = ManyRelationCascadeEnum.SaveUpdate)]
		public IList Groups
		{
			get{ return _groups; }
			set{ _groups = value; }
		}

		/// <summary>
		/// Hook to change the object state
		/// before saving it.
		/// </summary>
		/// <param name="state"></param>
		/// <returns>Return <c>true</c> if you have changed the state. <c>false</c> otherwise</returns>
		protected override bool BeforeSave(IDictionary state)
		{
			return base.BeforeSave(state);
		}

		/// <summary>
		/// Hook to transform the read data 
		/// from the database before populating 
		/// the object instance
		/// </summary>
		/// <param name="adapter"></param>
		/// <returns>Return <c>true</c> if you have changed the state. <c>false</c> otherwise</returns>
		protected override bool BeforeLoad(IDictionary adapter)
		{
			return base.BeforeLoad(adapter);
		}

		/// <summary>
		/// Hook to perform additional tasks 
		/// before removing the object instance representation
		/// from the database.
		/// </summary>
		/// <param name="adapter"></param>
		protected override void BeforeDelete(IDictionary adapter)
		{
			base.BeforeDelete(adapter);
		}

		/// <summary>
		/// Called before a flush
		/// </summary>
		protected override void PreFlush()
		{
			base.PreFlush();
		}

		/// <summary>
		/// Called after a flush that actually ends in execution of the SQL statements required to
		/// synchronize in-memory state with the database.
		/// </summary>
		protected override void PostFlush()
		{
			base.PostFlush();
		}

		/// <summary>
		/// Called when a transient entity is passed to <c>SaveOrUpdate</c>.
		/// </summary>
		/// <remarks>
		///	The return value determines if the object is saved
		///	<list>
		///		<item><c>true</c> - the entity is passed to <c>Save()</c>, resulting in an <c>INSERT</c></item>
		///		<item><c>false</c> - the entity is passed to <c>Update()</c>, resulting in an <c>UPDATE</c></item>
		///		<item><c>null</c> - Hibernate uses the <c>unsaved-value</c> mapping to determine if the object is unsaved</item>
		///	</list>
		/// </remarks>
		/// <returns></returns>
		protected override object IsUnsaved()
		{
			return base.IsUnsaved();
		}

		/// <summary>
		/// Called from <c>Flush()</c>. The return value determines whether the entity is updated
		/// </summary>
		/// <remarks>
		///		<list>
		///			<item>an array of property indicies - the entity is dirty</item>
		///			<item>an empty array - the entity is not dirty</item>
		///			<item><c>null</c> - use Hibernate's default dirty-checking algorithm</item>
		///		</list>
		/// </remarks>
		/// <param name="id"></param>
		/// <param name="previousState"></param>
		/// <param name="currentState"></param>
		/// <param name="types"></param>
		/// <returns>An array of dirty property indicies or <c>null</c> to choose default behavior</returns>
		protected override int[] FindDirty(object id, IDictionary previousState, IDictionary currentState, IType[] types)
		{
			return base.FindDirty(id, previousState, currentState, types);
		}

		public static void DeleteAll()
		{
			ActiveRecordMediator.DeleteAll(typeof (Group));
		}

		public static void DeleteAll(string where)
		{
			ActiveRecordMediator.DeleteAll(typeof (Group), where);
		}

		public static Group[] FindAll()
		{
			return (Group[]) ActiveRecordMediator.FindAll(typeof (Group));
		}

		public static Group Find(int id)
		{
			return (Group) ActiveRecordMediator.FindByPrimaryKey(typeof (Group), id);
		}

		public static int FetchCount()
		{
			return ActiveRecordBase.CountAll(typeof(Group));
		}

		public static int FetchCount(string filter, params object[] args)
		{
			return ActiveRecordBase.CountAll(typeof(Group), filter, args);
		}

		public static bool Exists()
		{
			return ActiveRecordBase.Exists(typeof(Group));
		}

		public static bool Exists(string filter, params object[] args)
		{
			return ActiveRecordBase.Exists(typeof(Group), filter, args);
		}

		/// <summary>
		/// Lifecycle method invoked during Save of the entity
		/// </summary>
		protected override void OnSave()
		{
			onSaveCalled = true;
		}

		/// <summary>
		/// Lifecycle method invoked during Update of the entity
		/// </summary>
		protected override void OnUpdate()
		{
			onUpdateCalled = true;
		}

		/// <summary>
		/// Lifecycle method invoked during Delete of the entity
		/// </summary>
		protected override void OnDelete()
		{
			onDeleteCalled = true;
		}

		/// <summary>
		/// Lifecycle method invoked during Load of the entity
		/// </summary>
		protected override void OnLoad(object id)
		{
			onLoadCalled = true;
		}

		public bool OnSaveCalled
		{
			get { return onSaveCalled; }
		}

		public bool OnUpdateCalled
		{
			get { return onUpdateCalled; }
		}

		public bool OnDeleteCalled
		{
			get { return onDeleteCalled; }
		}

		public bool OnLoadCalled
		{
			get { return onLoadCalled; }
		}

		public ISession CurrentSession
		{
			get
			{
				return (ISession) 
					ActiveRecordMediator.Execute(typeof(Group), new NHibernateDelegate(GrabSession), this);
			}
		}

		private object GrabSession(ISession session, object instance)
		{
			return session;
		}

		public void CustomAction()
		{
			ActiveRecordMediator.Execute(typeof (Group), new NHibernateDelegate(MyCustomMethod), this);
		}

		private object MyCustomMethod(ISession session, object blogInstance)
		{
			session.Delete(blogInstance);
			session.Flush();

			return null;
		}

		internal static ISessionFactoryHolder Holder
		{
			get { return ActiveRecordMediator.GetSessionFactoryHolder(); }
		}
	}
}