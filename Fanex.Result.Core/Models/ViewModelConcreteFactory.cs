// -----------------------------------------------------------------------
// <copyright file="ViewModelConcreteFactory.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.Result.Core.Models
{
    using System;
    using Fanex.Result.Core.DTO;
    using Fanex.Result.Core.Exception;
    using Fanex.Result.Core.Extension;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    internal sealed class ViewModelConcreteFactory : ViewModelFactory
    {
        private const string EXTNAME = "ViewModel";
        private const string NAMESPACE = "Fanex.Result.Core.ViewModels.";

        private static readonly object SynceObject = new object();

        private static ViewModelFactory instance;

        private ViewModelConcreteFactory()
        {
        }

        public static ViewModelFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (SynceObject)
                    {
                        if (instance == null)
                        {
                            instance = new ViewModelConcreteFactory();
                        }
                    }
                }

                return instance;
            }
        }

        /// <summary>
        /// Get view model.
        /// Used for render html result.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="sportType">The sport type.</param>
        /// <param name="languageCode">The language code.</param>
        /// <param name="resource">The language resource.</param>
        /// <returns>The view model.</returns>
        public override object GetViewModel(object obj, SportType sportType, string languageCode, LangResources resource)
        {
            var dtoName = sportType.StringMe();
            var viewModel = Type.GetType(NAMESPACE + dtoName + EXTNAME);

            if (viewModel == null)
            {
                throw new ObjectNullException(dtoName);
            }

            return Activator.CreateInstance(viewModel, obj, languageCode, resource);
        }

        public override object GetOutRightViewModel(object obj, string languageCode, LangResources resource)
        {
            const string DtoName = "OutRight";
            Type viewModel = Type.GetType(NAMESPACE + DtoName + EXTNAME);
            if (viewModel == null)
            {
                throw new ObjectNullException(DtoName);
            }

            return Activator.CreateInstance(viewModel, obj, languageCode, resource);
        }

        public override object GetParlayViewModel(object obj, string languageCode, LangResources resource)
        {
            const string DtoName = "Parlay";
            Type viewModel = Type.GetType(NAMESPACE + DtoName + EXTNAME);
            if (viewModel == null)
            {
                throw new ObjectNullException(DtoName);
            }

            return Activator.CreateInstance(viewModel, obj, languageCode, resource);
        }
    }
}