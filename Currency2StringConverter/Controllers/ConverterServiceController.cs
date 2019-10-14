using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Currency2StringConverter.ConverterServiceReference;

namespace Currency2StringConverter.Controllers
{
    public class ConverterServiceController: IDisposable
    {
        private static readonly object syncObject = new object();
        private static ConverterServiceController controller = null;

        public static ConverterServiceController Controller
        {
            get
            {
                if(controller == null)
                    lock (syncObject)
                    {
                        controller = controller ?? (controller = new ConverterServiceController());
                    }
                return controller;
            }
        }

        private ClientBase<IConverterService> service;

        private ConverterServiceController() { }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                try
                {
                    if (service.State == System.ServiceModel.CommunicationState.Opened
                        || service.State == System.ServiceModel.CommunicationState.Opening)
                        service.Close();
                }
                catch (Exception ex)
                { }
                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ConverterServiceController()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public IConverterService GetService()
        {
            if (service == null)
                service = new ConverterServiceClient();
            return service as IConverterService;
        }


    }
}
