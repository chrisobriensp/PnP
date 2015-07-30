using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Provisioning.Common.Data;
using Provisioning.Common;
using System.Collections.Generic;
using Provisioning.Common.Data.SiteRequests;
using System.Configuration;

namespace ProvisioningTests._5_COB
{
    [TestClass]
    public class SiteCreationTests
    {
        [TestMethod]
        [TestCategory("Site Request Manager")]
        public void CreateSiteFromStaticInfo()
        {
            ISiteRequestFactory _actualFactory = SiteRequestFactory.GetInstance();
            var _manager = _actualFactory.GetSiteRequestManager();
            
            var _siteInfo = this.GetSiteRequestMock();
            _manager.CreateNewSiteRequest(_siteInfo);
        }

        public SiteRequestInformation GetSiteRequestMock()
        {
            var _owner = new SiteUser()
            {
                Name = "cob@chrisobrien.com"
            };
            //Add addtional Users
            List<SiteUser> _additionalAdmins = new List<SiteUser>();
            SiteUser _admin1 = new SiteUser();
            _admin1.Name = "suzannejohnson@chrisobrien.com";
            _additionalAdmins.Add(_admin1);
            
            //SiteUser _admin2 = new SiteUser();
            ////  _admin2.Email = "frank@microsoftacs.onmicrosoft.com";
            //_admin2.Name = "user2@contoso.com";
            //_additionalAdmins.Add(_admin2);

            var _siteInfo = new SiteRequestInformation()
            {
                Title = "PnP provisioned site 1",
                Description = "PnP_Provisioned1 Description",
                Template = "CT2",
                Url = ConfigurationManager.AppSettings["SPHost"] + "/sites/PnP_Provisioned1",
                SitePolicy = "HBI",
                SiteOwner = _owner,
                AdditionalAdministrators = _additionalAdmins,
                EnableExternalSharing = true,
                SharePointOnPremises = true
            };

            return _siteInfo;

        }
    }
}
