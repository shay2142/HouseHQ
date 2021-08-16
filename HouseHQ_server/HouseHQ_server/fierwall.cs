using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;

namespace HouseHQ_server
{
    class fierwall
    {
        public void createRulefierwall()
        {
            Type tNetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(tNetFwPolicy2);
            var currentProfiles = fwPolicy2.CurrentProfileTypes;

            // Let's create a new rule
            INetFwRule2 inboundRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
            inboundRule.Enabled = true;
            //Allow through firewall
            inboundRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
            //Using protocol TCP
            inboundRule.Protocol = 6; // TCP
                                      //Port 8080
            inboundRule.LocalPorts = "8080";
            //Name of rule
            inboundRule.Name = "HHQ";
            // ...//
            inboundRule.Profiles = currentProfiles;

            // Now add the rule
            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
            firewallPolicy.Rules.Add(inboundRule);
        }
    }
}
