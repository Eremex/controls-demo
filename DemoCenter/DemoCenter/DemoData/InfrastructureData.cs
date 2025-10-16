using System.Collections.ObjectModel;

namespace DemoCenter.DemoData
{
    public enum AssetType
    {
        DataCenter,
        ServerRack,
        VirtualizationHost,
        DatabaseServer,
        WebServer,
        StorageSystem,
        NetworkDevice,
        SecurityAppliance
    }

    public enum AssetStatus
    {
        Operational,
        Maintenance,
        Degraded,
        Offline,
        Decommissioned,
        Critical,
        Warning
    }

    public class InfrastructureItem
    {
        public string Name { get; set; }
        public AssetType AssetType { get; set; }

        public DateTime? LastMaintenance { get; set; }
        public bool IsOperational => Status == AssetStatus.Operational || Status == AssetStatus.Warning || Status == AssetStatus.Critical;
        public AssetStatus Status { get; set; }

        public float Utilization { get; set; }
        public decimal MaintenanceCost { get; set; }
        public decimal PowerConsumption { get; set; }

        public ObservableCollection<InfrastructureItem> Children { get; } = new();

        public bool HasChildren => Children.Count > 0;
    }

    public class InfrastructureData
    {
        private static readonly AssetType[] serverTypes = { AssetType.VirtualizationHost, AssetType.DatabaseServer, AssetType.WebServer, AssetType.StorageSystem, AssetType.NetworkDevice, AssetType.SecurityAppliance  };

        private static readonly string[] DataCenterNames = { "Quantum Data Center", "Aether Data Center", "Stellar Data Center", "Nova Data Center", "Infinity Data Center" };

        private static readonly string[] ServerNames = {  "HyperCore", "VMHost", "CloudNode", "VirtualEngine", "Orchestrator", "DataForge", "QueryMaster", "StorageBrain", "ArchiveKeeper", "InfoStream",
                                                          "WebFlux", "SiteStream", "PageServer", "APIGateway", "ContentHub","AppEngine", "ServiceNode", "RuntimeCore", "MicroHost", "WorkflowUnit",
                                                          "StoragePod", "DataVault", "FileArray", "DiskCluster", "ArchiveBank","NetSwitch", "DataRouter", "TrafficHub", "PacketFlow", "ConnectionPoint",
                                                          "Firewall", "SecurityShield", "ThreatGuard", "AccessControl", "CyberDefense","ComputeNode", "ServerUnit", "ProcessingCore", "ExecutionEngine", "TaskHandler" };
                                                           
    
        public static List<InfrastructureItem> GenerateData()
        {
            var items = new List<InfrastructureItem>();

            var random = new Random(40);

            for (int i = 1; i <= 5; i++)
            {
                var dataCenter = CreateDataCenter(random, i);

                for (int j = 1; j <= random.Next(8, 13); j++)
                {
                    var serverRack = CreateServerRack(random, j);

                    for (int k = 1; k <= random.Next(6, 11); k++)
                    {
                        var serverComponent = CreateServer(random, k);

                        serverRack.Children.Add(serverComponent);
                    }

                    dataCenter.Children.Add(serverRack);
                }

                items.Add(dataCenter);
            }
            return items;
        }

        private static InfrastructureItem CreateDataCenter(Random random, int index)
        {
            return new InfrastructureItem()
            {
                Name = DataCenterNames[index - 1],
                AssetType = AssetType.DataCenter,
                LastMaintenance = DateTime.Now.AddMonths(-random.Next(1, 6)),
                Status = GetRandomStatus(random, 0.8),
                Utilization = random.Next(60, 95) / 100f,
                MaintenanceCost = random.Next(50000, 200000),
                PowerConsumption = random.Next(200, 500),
            };
        }

        private static InfrastructureItem CreateServerRack(Random random, int index)
        {
            return new InfrastructureItem
            {
                Name = $"Rack {index:00}",
                AssetType = AssetType.ServerRack,
                LastMaintenance = DateTime.Now.AddMonths(-random.Next(1, 12)),
                Status = GetRandomStatus(random, 0.7),
                Utilization = random.Next(70, 98) / 100f,
                MaintenanceCost = random.Next(5000, 15000),
                PowerConsumption = random.Next(5, 15),
            };
        }

        private static InfrastructureItem CreateServer(Random random, int index)
        {
            var serverType = serverTypes[random.Next(serverTypes.Length)];

            var server = new InfrastructureItem
            {
                Name = GetServerName(random, serverType),
                AssetType = serverType,
                LastMaintenance = DateTime.Now.AddMonths(-random.Next(1, 18)),
                Status = GetRandomStatus(random, 0.6),
                Utilization = random.Next(30, 95) / 100f,
                MaintenanceCost = GetServerMaintenanceCost(random, serverType),
                PowerConsumption = Math.Round((decimal)random.NextDouble() * 2 + 0.5m, 1)
            };

            return server;
        }

        private static AssetStatus GetRandomStatus(Random random, double operationalChance)
        {
            var rand = random.NextDouble();
            if (rand < operationalChance * 0.7) return AssetStatus.Operational;
            if (rand < operationalChance * 0.8) return AssetStatus.Warning;
            if (rand < operationalChance * 0.9) return AssetStatus.Maintenance;
            if (rand < operationalChance) return AssetStatus.Degraded;
            return AssetStatus.Offline;
        }

        private static string GetServerName(Random random, AssetType type)
        {
            return ServerNames[(int)type * 5 + random.Next(5)] + " Server";
        }

        private static decimal GetServerMaintenanceCost(Random random, AssetType type)
        {
            return random.Next(20000, 80000) * (decimal)(random.NextDouble() * 0.15);
        }
    }
}
