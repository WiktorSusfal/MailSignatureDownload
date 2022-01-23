using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;


namespace MailSignatureDownload
{
    /// <summary>
    /// Main View Model For Application. Contains MailSignature in HTML format and command to retrieve it from OWA.
    /// </summary>
    public class ViewModel : ObservableObject
    {

        private string _mailSignatureHtml;
        public RelayCommand GetSignatureCmd { get; private set; }

        public ViewModel()
        {
            _mailSignatureHtml = string.Empty;
            GetSignatureCmd = new RelayCommand(GetSignature);
        }

        public string MailSignatureHtml
        {
            get => _mailSignatureHtml;

            set
            {
                if (value != null)
                {
                    _mailSignatureHtml = value;
                    OnPropChanged(nameof(MailSignatureHtml));
                }
            }
        }

        public void GetSignature(object o)
        {
            ExchangeService eService = new ExchangeService(ExchangeVersion.Exchange2010_SP1);
            
            eService.Credentials = new WebCredentials("wiktor.susfal@outlook.com", "Aaaaaa1!");
            eService.UseDefaultCredentials = false;
            eService.TraceEnabled = true;
            eService.TraceFlags = TraceFlags.All;
            eService.AutodiscoverUrl("wiktor.susfal@outlook.com", RedirectionUrlValidationCallback);

            var rootFolder = Folder.Bind(eService, WellKnownFolderName.Root);
            var owaConfig = UserConfiguration.Bind(eService, "OWA.UserOptions", rootFolder.ParentFolderId, UserConfigurationProperties.All);
            
            //Get email signature
            var signature = owaConfig.Dictionary["signaturehtml"];

            MailSignatureHtml = (string)signature;
        }

        private static bool RedirectionUrlValidationCallback(string redUrl)
        {

            bool result = false;

            //redairectionUrl
            Uri redUri = new Uri(redUrl);

            if (redUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }
    }
}
