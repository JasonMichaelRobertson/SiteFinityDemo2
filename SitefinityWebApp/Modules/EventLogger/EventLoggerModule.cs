using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Security.Events;
using Telerik.Sitefinity.Modules.Libraries.Web.Events;
using Telerik.Sitefinity.Modules.Forms.Events;
using System.Text;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Forums.Events;
using System.ComponentModel;
using Telerik.Sitefinity;
using SitefinityWebApp.Modules.EventLogger.Configuration;

namespace SitefinityWebApp.Modules.EventLogger
{
    /// <summary>
    /// Sitefinity module for logging application and data events
    /// </summary>
    public class EventLoggerModule : ModuleBase
    {


        /// <summary>
        /// Installs the specified initializer.
        /// </summary>
        /// <param name="initializer">The initializer.</param>
        public override void Install(SiteInitializer initializer)
        {
            var pageMgr = initializer.PageManager;

            #region Install Admin Pages

            // install the module using Fluent API
            var module = initializer.Installer;

            // add page to installed module
            module.CreateModulePage(this.LandingPageId, EventLoggerModule.ModuleName).
                        ShowInNavigation().
                        SetTitle(EventLoggerModule.ModuleName).
                        SetHtmlTitle(EventLoggerModule.ModuleName).
                        SetDescription(EventLoggerModule.ModuleName).
                        SetUrlName(EventLoggerModule.ModuleName).
                        PlaceUnder(CommonNode.System);
            #endregion

            #region Install Admin Controls

            // initialize user control using path
            var control = pageMgr.CreateControl<PageControl>("~/Modules/EventLogger/Admin/EventLoggerAdmin.ascx", "Content");

            // get page node and add user control
            var node = pageMgr.GetPageNode(this.LandingPageId);
            node.Page.Controls.Add(control);

            #endregion
        }

        /// <summary>
        /// Initializes this module, registers all event listeners.
        /// </summary>
        /// <param name="settings">The module settings.</param>
        public override void Initialize(ModuleSettings settings)
        {
            base.Initialize(settings);

            // register config
            App.WorkWith().Module(EventLoggerModule.ModuleName).Initialize().Configuration<EventLoggerConfig>();

            // generic data event
            EventHub.Subscribe<IDataEvent>(DataEvent_Handler);

            // User Events: Inherit from UserEventBase
            EventHub.Subscribe<UserCreating>(UserEvent_Handler);
            EventHub.Subscribe<UserCreated>(UserEvent_Handler);
            EventHub.Subscribe<UserUpdating>(UserEvent_Handler);
            EventHub.Subscribe<UserUpdated>(UserEvent_Handler);
            EventHub.Subscribe<UserDeleting>(UserEvent_Handler);
            EventHub.Subscribe<UserDeleted>(UserEvent_Handler);
            EventHub.Subscribe<PasswordRecoveryRequested>(UserEvent_Handler);

            // Profile Events: Inherit from ProfileEventBase
            EventHub.Subscribe<ProfileCreating>(ProfileEvent_Handler);
            EventHub.Subscribe<ProfileCreated>(ProfileEvent_Handler);
            EventHub.Subscribe<ProfileUpdating>(ProfileEvent_Handler);
            EventHub.Subscribe<ProfileUpdated>(ProfileEvent_Handler);
            EventHub.Subscribe<ProfileDeleting>(ProfileEvent_Handler);
            EventHub.Subscribe<ProfileDeleted>(ProfileEvent_Handler);

            // Media Events
            EventHub.Subscribe<IMediaContentDownloadingEvent>(MediaContentDownloadEvent_Handler);
            EventHub.Subscribe<IMediaContentDownloadedEvent>(MediaContentDownloadEvent_Handler);

            // Form Events
            EventHub.Subscribe<IFormEntryCreatedEvent>(FormEntryCreatedEvent_Handler);

            // Forum Events
            EventHub.Subscribe<IForumGroupDataEvent>(ForumGroupEvent_Handler);
            EventHub.Subscribe<IForumDataEvent>(ForumEvent_Handler);
            EventHub.Subscribe<IForumThreadDataEvent>(ForumThreadEvent_Handler);
            EventHub.Subscribe<IForumPostDataEvent>(ForumPostEvent_Handler);
            EventHub.Subscribe<IForumAttachmentEvent>(ForumAttachmentEvent_Handler);
            EventHub.Subscribe<IForumSubscriptionCreatedEvent>(ForumSubscriptionCreatedEvent_Handler);
            EventHub.Subscribe<IForumSubscriptionDeletedEvent>(ForumSubscriptionDeletedEvent_Handler);
            EventHub.Subscribe<IForumThreadSubscriptionCreatedEvent>(ForumThreadSubscriptionCreatedEvent_Handler);
            EventHub.Subscribe<IForumThreadSubscriptionDeletedEvent>(ForumThreadSubscriptionDeletedEvent_Handler);

            LoggerHelper.WriteLog("Event Logger Module Initializing\n\n");
        }

        #region Event Handlers

        /// <summary>
        /// Handler for Sitefinity content module generic data event.
        /// </summary>
        /// <param name="EventData">The event data.</param>
        private void DataEvent_Handler(IDataEvent EventData)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Event Data Type: {0}\n", EventData.GetType().ToString());
            sb.AppendFormat("	Action: {0}\n", EventData.Action);
            sb.AppendFormat("	Item Type: {0}\n", EventData.ItemType.FullName);
            sb.AppendFormat("	Item Id: {0}\n", EventData.ItemId);
            sb.AppendFormat("	Provider: {0}\n", EventData.ProviderName);
            sb.AppendFormat("	Origin: {0}\n", EventData.Origin);

            if (EventData is ILifecycleEvent)
                AppendLifeCycleData(EventData, sb);

            if (EventData is IMultilingualEvent)
                AppendMultilingualData(EventData, sb);

            if (EventData is IPropertyChangeDataEvent)
                AppendPropertyChangedData(EventData, sb);

            LoggerHelper.WriteLog(sb.ToString());
        }

        /// <summary>
        /// Appends the property changed data to the log.
        /// </summary>
        /// <param name="EventData">The event data.</param>
        /// <param name="sb">StringBuilder containing the current log.</param>
        private void AppendPropertyChangedData(IDataEvent EventData, StringBuilder sb)
        {
            var changed = EventData as IPropertyChangeDataEvent;
            if (changed.ChangedProperties.Count > 0)
            {
                sb.Append("	Changed Properties:\n");
                foreach (var property in changed.ChangedProperties)
                    sb.AppendFormat("		Property: {0}\n", property);
            }
        }

        /// <summary>
        /// Appends the multilingual data to the log
        /// </summary>
        /// <param name="EventData">The event data.</param>
        /// <param name="sb">StringBuilder containing the current log.</param>
        private void AppendMultilingualData(IDataEvent EventData, StringBuilder sb)
        {
            var multilingual = EventData as IMultilingualEvent;
            sb.AppendFormat("	Multilingual Info:\n");
            sb.AppendFormat("		Language: {0}\n", multilingual.Language);
        }

        /// <summary>
        /// Appends the life cycle data to the log
        /// </summary>
        /// <param name="EventData">The event data.</param>
        /// <param name="sb">StringBuilder containing the current log.</param>
        private void AppendLifeCycleData(IDataEvent EventData, StringBuilder sb)
        {
            var lifecycle = EventData as ILifecycleEvent;
            sb.Append("	Lifecycle Info:\n");
            sb.AppendFormat("		Status: {0}\n", lifecycle.Status);
            sb.AppendFormat("		OriginalId: {0}\n", lifecycle.OriginalContentId);
        }

        #region User Events

        /// <summary>
        /// Handler for all Sitefinity User events
        /// </summary>
        /// <param name="EventData">The event data.</param>
        private void UserEvent_Handler(UserEventBase EventData)
        {
            var sb = new StringBuilder();

            // log basic event data
            sb.AppendFormat("User Event Type: {0}\n", EventData.GetType().ToString());
            sb.AppendFormat("	UserName: {0}\n", EventData.UserName);
            sb.AppendFormat("	UserID: {0}\n", EventData.UserId);
            sb.AppendFormat("	Email: {0}\n", EventData.Email);
            sb.AppendFormat("	PasswordFormat: {0}\n", EventData.PasswordFormat);
            sb.AppendFormat("	IsApproved: {0}\n", EventData.IsApproved);
            sb.AppendFormat("	Provider: {0}\n", EventData.MembershipProviderName);
            sb.AppendFormat("	Origin: {0}\n", EventData.Origin);

            // "Creating" event contains the User object
            if (EventData is UserCreating)
            {
                // cast to access additional properties
                var createData = EventData as UserCreating;

                // If you need the User object it is available here:
                var user = createData.User;
            }

            // "Updating" event contains additional properties, as well as the User object
            if (EventData is UserUpdating)
            {
                // cast to access additional properties
                var updateData = EventData as UserUpdating;
                sb.AppendFormat("	ApprovalStatusChanged: {0}\n", updateData.ApprovalStatusChanged);
                sb.AppendFormat("	PasswordChanged: {0}\n", updateData.PasswordChanged);

                // If you need the User object it is available here:
                var user = updateData.User;
            }

            // "Deleting" event contains the User object
            if (EventData is UserDeleting)
            {
                // cast to access additional properties
                var deleteData = EventData as UserDeleting;

                // If you need the User object it is available here:
                var user = deleteData.User;
            }

            // append log
            LoggerHelper.WriteLog(sb.ToString());
        }

        #endregion

        #region Profile Events

        /// <summary>
        /// Handler for all Sitefinity profile events
        /// </summary>
        /// <param name="EventData">The event data.</param>
        private void ProfileEvent_Handler(ProfileEventBase EventData)
        {
            var sb = new StringBuilder();

            // log generic profile data
            sb.AppendFormat("User Event Type: {0}\n", EventData.GetType().ToString());
            sb.AppendFormat("	ProfileId: {0}\n", EventData.ProfileId);
            sb.AppendFormat("	ProfileType: {0}\n", EventData.ProfileType);
            sb.AppendFormat("	UserId: {0}\n", EventData.UserId);
            sb.AppendFormat("	Provider: {0}\n", EventData.MembershipProviderName);
            sb.AppendFormat("	Origin: {0}\n", EventData.Origin);

            // "Creating" event contains the full UserProfile object
            if (EventData is ProfileCreating)
            {
                // cast to access additional properties
                var createData = EventData as ProfileCreating;
                var profile = createData.Profile;

                sb.Append("	Profile Data:\n");
                //sb.AppendFormat("		Title: {0}\n", profile.Title);
                //sb.AppendFormat("		Description: {0}\n", profile.Description);
                //sb.AppendFormat("		UrlName: {0}\n", profile.UrlName);
                //sb.AppendFormat("		Visible: {0}\n", profile.Visible);

                // If you need the User object it is available here:
                var user = profile.User;
            }

            // "Updating" event contains the full UserProfile object
            if (EventData is ProfileUpdating)
            {

                // cast to access additional properties
                var updateData = EventData as ProfileUpdating;
                var profile = updateData.Profile;

                sb.Append("	Profile Data:\n");
                //sb.AppendFormat("		Title: {0}\n", profile.Title);
                //sb.AppendFormat("		Description: {0}\n", profile.Description);
                //sb.AppendFormat("		UrlName: {0}\n", profile.UrlName);
                //sb.AppendFormat("		Visible: {0}\n", profile.Visible);

                // If you need the User object it is available here:
                var user = profile.User;
            }

            // "Deleting" event contains the full UserProfile object
            if (EventData is ProfileDeleting)
            {
                // cast to access additional properties
                var deleteData = EventData as ProfileDeleting;
                var profile = deleteData.Profile;

                sb.Append("	Profile Data:\n");
                //sb.AppendFormat("		Title: {0}\n", profile.Title);
                //sb.AppendFormat("		Description: {0}\n", profile.Description);
                //sb.AppendFormat("		UrlName: {0}\n", profile.UrlName);
                //sb.AppendFormat("		Visible: {0}\n", profile.Visible);

                // If you need the User object it is available here:
                var user = profile.User;
            }

            LoggerHelper.WriteLog(sb.ToString());
        }

        #endregion

        #region MediaEvents

        /// <summary>
        /// Handler for all Media Download events
        /// </summary>
        /// <param name="EventData">The event data.</param>
        private void MediaContentDownloadEvent_Handler(IMediaContentDownloadEvent EventData)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("User Event Type: {0}\n", EventData.GetType().ToString());
            sb.AppendFormat("	LibraryId: {0}\n", EventData.LibraryId);
            sb.AppendFormat("	FileId: {0}\n", EventData.FileId);
            sb.AppendFormat("	UserId: {0}\n", EventData.UserId);
            sb.AppendFormat("	Type: {0}\n", EventData.Type);
            sb.AppendFormat("	MimeType: {0}\n", EventData.MimeType);
            sb.AppendFormat("	Title: {0}\n", EventData.Title);
            sb.AppendFormat("	Url: {0}\n", EventData.Url);
            sb.AppendFormat("	Provider: {0}\n", EventData.ProviderName);
            sb.AppendFormat("	Origin: {0}\n", EventData.Origin);

            // add headers
            sb.Append("	Headers: {0}\n");
            foreach (string key in EventData.Headers)
                sb.AppendFormat("		{0}: {1}\n", key, EventData.Headers[key]);

            LoggerHelper.WriteLog(sb.ToString());
        }

        #endregion

        #region Form Events

        /// <summary>
        /// Handles the event triggered by a user submitting a Sitefinity form
        /// </summary>
        /// <param name="EventData">The event data.</param>
        private void FormEntryCreatedEvent_Handler(IFormEntryCreatedEvent EventData)
        {
            var sb = new StringBuilder();

            // append generic event data
            sb.AppendFormat("User Event Type: {0}\n", EventData.GetType().ToString());
            sb.AppendFormat("	Form Id: {0}\n", EventData.FormId);
            sb.AppendFormat("	Form name: {0}\n", EventData.FormName);
            sb.AppendFormat("	SubscriptionListId: {0}\n", EventData.FormSubscriptionListId);
            sb.AppendFormat("	Form Title: {0}\n", EventData.FormTitle);
            sb.AppendFormat("	Origin: {0}\n", EventData.Origin);

            LoggerHelper.WriteLog(sb.ToString());
        }


        #endregion

        #region Forum Events

        private void ForumGroupEvent_Handler(IForumGroupDataEvent EventData)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Forum Group Event Type: {0}\n", EventData.GetType().ToString());

            // basic data is in the ForumDataEventBase
            var eventBase = (ForumDataEventBase)EventData;
            sb.AppendFormat("	Action: {0}\n", eventBase.Action);
            sb.AppendFormat("	Type: {0}\n", eventBase.ItemType);
            sb.AppendFormat("	ItemId: {0}\n", eventBase.ItemId);
            sb.AppendFormat("	Provider: {0}\n", eventBase.ProviderName);
            sb.AppendFormat("	Origin: {0}\n", eventBase.Origin);

            // append additional Forum Group properties
            sb.AppendFormat("	Forum Group ID: {0}\n", EventData.ForumGroupId);
            sb.AppendFormat("	UserID: {0}\n", EventData.UserId);
            sb.AppendFormat("	Title: {0}\n", EventData.Title);
            sb.AppendFormat("	Visible: {0}\n", EventData.Visible);

            // "Created" event contains the Creation Date
            if (EventData is IForumGroupCreatedEvent)
            {
                // cast to access additional properties
                var createData = EventData as IForumGroupCreatedEvent;

                sb.AppendFormat("	Created: {0}\n", createData.CreationDate);
            }

            // "Updated" event contains Modification date
            if (EventData is IForumGroupUpdatedEvent)
            {
                // cast to access additional properties
                var updateData = EventData as IForumGroupUpdatedEvent;
                sb.AppendFormat("	Modified: {0}\n", updateData.ModificationDate);
            }

            // "Deleted" event contains the Deletion date
            if (EventData is IForumGroupDeletedEvent)
            {
                // cast to access additional properties
                var deleteData = EventData as IForumGroupDeletedEvent;

                sb.AppendFormat("	Deleted: {0}\n", deleteData.DeletionDate);
            }

            // append log
            LoggerHelper.WriteLog(sb.ToString());
        }

        private void ForumEvent_Handler(IForumDataEvent EventData)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Forum Event Type: {0}\n", EventData.GetType().ToString());

            // basic data is in the ForumDataEventBase
            var eventBase = (ForumDataEventBase)EventData;
            sb.AppendFormat("	Action: {0}\n", eventBase.Action);
            sb.AppendFormat("	Type: {0}\n", eventBase.ItemType);
            sb.AppendFormat("	ItemId: {0}\n", eventBase.ItemId);
            sb.AppendFormat("	Provider: {0}\n", eventBase.ProviderName);
            sb.AppendFormat("	Origin: {0}\n", eventBase.Origin);

            // Append additional Forum info
            sb.AppendFormat("	UserId: {0}\n", EventData.UserId);

            // forum info is in the Item property
            sb.Append("	Forum Info:\n");
            sb.AppendFormat("		Title: {0}\n", EventData.Item.Title);
            sb.AppendFormat("		UrlName: {0}\n", EventData.Item.UrlName);

            //// "Created" event does not currently contain additional properties
            if (EventData is IForumCreatedEvent)
            {
                // cast to access additional properties
                var createData = EventData as IForumCreatedEvent;
            }

            // "Updated" event does not currently contain additional data
            if (EventData is IForumUpdatedEvent)
            {
                // cast to access additional properties
                var updateData = EventData as IForumUpdatedEvent;
            }

            // "Deleted" event contains contains the deletion date
            if (EventData is IForumDeletedEvent)
            {
                // cast to access additional properties
                var deleteData = EventData as IForumDeletedEvent;

                sb.AppendFormat("	Deleted: {0}\n", deleteData.DeletionDate);
            }

            // append log
            LoggerHelper.WriteLog(sb.ToString());
        }

        private void ForumThreadEvent_Handler(IForumThreadDataEvent EventData)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Forum Thread Event Type: {0}\n", EventData.GetType().ToString());

            // basic data is in the ForumDataEventBase
            var eventBase = (ForumDataEventBase)EventData;
            sb.AppendFormat("	Action: {0}\n", eventBase.Action);
            sb.AppendFormat("	Type: {0}\n", eventBase.ItemType);
            sb.AppendFormat("	ItemId: {0}\n", eventBase.ItemId);
            sb.AppendFormat("	Provider: {0}\n", eventBase.ProviderName);
            sb.AppendFormat("	Origin: {0}\n", eventBase.Origin);

            // Append Forum Thread data
            sb.AppendFormat("	UserId: {0}\n", EventData.UserId);
            sb.Append("	Forum Info:\n");
            sb.AppendFormat("		Title: {0}\n", EventData.Item.Title);
            sb.AppendFormat("		Forum: {0}\n", EventData.Item.Forum.Title);

            // "Created" event does not currently contain additional data
            if (EventData is IForumThreadCreatedEvent)
            {
                // cast to access additional properties
                var createData = EventData as IForumThreadCreatedEvent;
            }

            // "Updated" event does not currently contain additional data
            if (EventData is IForumThreadUpdatedEvent)
            {
                // cast to access additional properties
                var updateData = EventData as IForumThreadUpdatedEvent;
            }

            // "Deleted" event contains the Deletion date and Thread ID
            if (EventData is IForumThreadDeletedEvent)
            {
                // cast to access additional properties
                var deleteData = EventData as IForumThreadDeletedEvent;
                sb.AppendFormat("	PostID: {0}\n", deleteData.ThreadId);
                sb.AppendFormat("	Deleted: {0}\n", deleteData.DeletionDate);
            }

            // append log
            LoggerHelper.WriteLog(sb.ToString());
        }

        private void ForumPostEvent_Handler(IForumPostDataEvent EventData)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Forum Post Event Type: {0}\n", EventData.GetType().ToString());

            // basic data is in the ForumDataEventBase
            var eventBase = (ForumDataEventBase)EventData;
            sb.AppendFormat("	Action: {0}\n", eventBase.Action);
            sb.AppendFormat("	Type: {0}\n", eventBase.ItemType);
            sb.AppendFormat("	ItemId: {0}\n", eventBase.ItemId);
            sb.AppendFormat("	Provider: {0}\n", eventBase.ProviderName);
            sb.AppendFormat("	Origin: {0}\n", eventBase.Origin);

            // Append additional Forum Post data, Forum info is available in the Item property
            sb.AppendFormat("	UserId: {0}\n", EventData.UserId);
            sb.Append("	Forum Info:\n");
            sb.AppendFormat("		Title: {0}\n", EventData.Item.Title);
            sb.AppendFormat("		Forum: {0}\n", EventData.Item.Forum.Title);
            sb.AppendFormat("		Thread: {0}\n", EventData.Item.Thread.Title);

            // "Created" event does not currently contain additional data
            if (EventData is IForumPostCreatedEvent)
            {
                // cast to access additional properties
                var createData = EventData as IForumPostCreatedEvent;
            }

            // "Updated" event does not currently contain additional data
            if (EventData is IForumPostUpdatedEvent)
            {
                // cast to access additional properties
                var updateData = EventData as IForumPostUpdatedEvent;
            }

            // "Deleted" event contains the Deletion date and Post ID
            if (EventData is IForumPostDeletedEvent)
            {
                // cast to access additional properties
                var deleteData = EventData as IForumPostDeletedEvent;
                sb.AppendFormat("	PostID: {0}\n", deleteData.PostId);
                sb.AppendFormat("	Deleted: {0}\n", deleteData.DeletionDate);
            }

            // append log
            LoggerHelper.WriteLog(sb.ToString());
        }

        private void ForumAttachmentEvent_Handler(IForumAttachmentEvent EventData)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Forum Post Event Type: {0}\n", EventData.GetType().ToString());

            // basic data is in the ForumDataEventBase
            var eventBase = (ForumDataEventBase)EventData;
            sb.AppendFormat("	Action: {0}\n", eventBase.Action);
            sb.AppendFormat("	Type: {0}\n", eventBase.ItemType);
            sb.AppendFormat("	ItemId: {0}\n", eventBase.ItemId);
            sb.AppendFormat("	Provider: {0}\n", eventBase.ProviderName);
            sb.AppendFormat("	Origin: {0}\n", eventBase.Origin);

            // append attachment data, Item property is a ContentLink with the attached data
            sb.Append("	Attachment Info:\n");
            sb.AppendFormat("		Item Id: {0}\n", EventData.Item.Id);
            sb.AppendFormat("		Type: {0}\n", EventData.Item.ChildItemType);

            // append attachment attributes
            foreach (var key in EventData.Item.Attributes)
                sb.AppendFormat("		{0}: {1}\n", key.Key, key.Value);

            // "Created" event does not currently contain additional data
            if (EventData is IForumAttachmentCreatedEvent)
            {
                // cast to access additional properties
                var createData = EventData as IForumAttachmentCreatedEvent;
            }

            // "Deleted" event contains the deletion data
            if (EventData is IForumAttachmentDeletedEvent)
            {
                // cast to access additional properties
                var deleteData = EventData as IForumAttachmentDeletedEvent;
                sb.AppendFormat("		Deleted: {0}\n", deleteData.DeletionDate);
            }

            // append log
            LoggerHelper.WriteLog(sb.ToString());
        }

        private void ForumSubscriptionCreatedEvent_Handler(IForumSubscriptionCreatedEvent EventData)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Forum Subscription Event Type: {0}\n", EventData.GetType().ToString());
            sb.AppendFormat("	Forum Id: {0}\n", EventData.ForumId);
            sb.AppendFormat("	Subscription Created: {0}\n", EventData.CreationDate);
            sb.AppendFormat("	Forum Provider: {0}\n", EventData.ForumProvider);
            sb.AppendFormat("	User Id: {0}\n", EventData.UserId);
            sb.AppendFormat("	User Provider: {0}\n", EventData.UserProvider);
            sb.AppendFormat("	Origin: {0}\n", EventData.Origin);

            // append log
            LoggerHelper.WriteLog(sb.ToString());
        }

        private void ForumSubscriptionDeletedEvent_Handler(IForumSubscriptionDeletedEvent EventData)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Forum Subscription Event Type: {0}\n", EventData.GetType().ToString());
            sb.AppendFormat("	Forum Id: {0}\n", EventData.ForumId);
            sb.AppendFormat("	Subscription Deleted: {0}\n", EventData.DeletionDate);
            sb.AppendFormat("	Forum Provider: {0}\n", EventData.ForumProvider);
            sb.AppendFormat("	User Id: {0}\n", EventData.UserId);
            sb.AppendFormat("	User Provider: {0}\n", EventData.UserProvider);
            sb.AppendFormat("	Origin: {0}\n", EventData.Origin);

            // append log
            LoggerHelper.WriteLog(sb.ToString());
        }

        private void ForumThreadSubscriptionCreatedEvent_Handler(IForumThreadSubscriptionCreatedEvent EventData)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Forum Subscription Event Type: {0}\n", EventData.GetType().ToString());
            sb.AppendFormat("	Forum Thread Id: {0}\n", EventData.ForumThreadId);
            sb.AppendFormat("	Subscription Created: {0}\n", EventData.CreationDate);
            sb.AppendFormat("	Forum Thread Provider: {0}\n", EventData.ForumThreadProvider);
            sb.AppendFormat("	User Id: {0}\n", EventData.UserId);
            sb.AppendFormat("	User Provider: {0}\n", EventData.UserProvider);
            sb.AppendFormat("	Origin: {0}\n", EventData.Origin);

            // append log
            LoggerHelper.WriteLog(sb.ToString());
        }

        private void ForumThreadSubscriptionDeletedEvent_Handler(IForumThreadSubscriptionDeletedEvent EventData)
        {
            var sb = new StringBuilder();

            sb.AppendFormat("Forum Subscription Event Type: {0}\n", EventData.GetType().ToString());
            sb.AppendFormat("	Forum Thread Id: {0}\n", EventData.ForumThreadId);
            sb.AppendFormat("	Subscription Created: {0}\n", EventData.DeletionDate);
            sb.AppendFormat("	Forum Thread Provider: {0}\n", EventData.ForumThreadProvider);
            sb.AppendFormat("	User Id: {0}\n", EventData.UserId);
            sb.AppendFormat("	User Provider: {0}\n", EventData.UserProvider);
            sb.AppendFormat("	Origin: {0}\n", EventData.Origin);

            // append log
            LoggerHelper.WriteLog(sb.ToString());
        }

        #endregion

        #endregion

        /// <summary>
        /// Upgrades the module from a previous version. Not currently used.
        /// </summary>
        /// <param name="initializer">The Sitefinity initializer.</param>
        /// <param name="upgradeFrom">The version from which to upgrade.</param>
        public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
        {
            // not needed
        }

        /// <summary>
        /// Gets the module configuration
        /// </summary>
        /// <returns></returns>
        protected override ConfigSection GetModuleConfig()
        {
            return Config.Get<EventLoggerConfig>();
        }

        /// <summary>
        /// Gets the landing page id for the admin page
        /// </summary>
        public override Guid LandingPageId
        {
            get { return new Guid("F9D8AA6F-ECF6-4417-8CB1-70C38D47DB26"); }
        }

        /// <summary>
        /// Gets the module content managers. Returns null as managers are not used in this module.
        /// </summary>
        public override Type[] Managers
        {
            get { return null; }
        }

        public static readonly string ModuleName = "EventLogger";
    }
}