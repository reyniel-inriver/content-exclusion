namespace TestApp.TestCases
{
    using System;
    using System.Collections.Generic;
    using inRiver.Remoting;
    using inRiver.Remoting.Cache;
    using inRiver.Remoting.Objects;

    public class RemoteManagerWrapper
    {
        private static RemoteManagerWrapper instance;

        private RemoteManager manager;

        private RemoteManagerWrapper(RemoteManager manager)
        {
            this.manager = manager;
        }

        public static DataService DataService => RemoteManager.DataService as DataService;

        public static UserService UserService => RemoteManager.UserService as UserService;

        public static UtilityService UtilityService => RemoteManager.UtilityService as UtilityService;

        public static ModelService ModelService => RemoteManager.ModelService as ModelService;

        public static ChannelService ChannelService => RemoteManager.ChannelService as ChannelService;

        public static bool IsInitialized => instance != null;

        public static RemoteManager RemoteManager
        {
            get {
                if (instance == null)
                {
                    throw new InvalidOperationException(
                        "RemoteManager not initialized. Please call RemoteManager.CreateInstance before using RemoteManager");
                }

                return instance.manager;
            }
        }

        public static RemoteManagerWrapper CreateInstance(RemoteManager manager, bool useCache = false)
        {
            if (instance == null)
            {
                instance = new RemoteManagerWrapper(manager);
            }
            else
            {
                instance.manager = manager;
            }

            if (!useCache)
            {
                return instance;
            }

            var cache = new CacheContainer { LatestUpdate = DateTime.UtcNow };

            // cache.SetServerSettings(manager.UtilityService.GetAllServerSettings());
            cache.SetLanguages(manager.UtilityService.GetAllLanguages());
            cache.SetCVLs(manager.ModelService.GetAllCVLs());
            cache.SetCategories(manager.ModelService.GetAllCategories());
            cache.SetEntityTypes(manager.ModelService.GetAllEntityTypes());
            cache.SetFieldSets(manager.ModelService.GetAllFieldSets());
            cache.SetFieldTypes(manager.ModelService.GetAllFieldTypes());

            cache.SetLinkTypes(manager.ModelService.GetAllLinkTypes());

            var temp = new Dictionary<string, List<CVLValue>>();

            foreach (var value in manager.ModelService.GetAllCVLValues())
            {
                if (!temp.ContainsKey(value.CVLId))
                {
                    temp.Add(value.CVLId, new List<CVLValue>());
                }

                temp[value.CVLId].Add(value);
            }

            cache.SetCVLValues(temp);

            instance.manager.SetCache(cache);

            return instance;
        }
    }
}
