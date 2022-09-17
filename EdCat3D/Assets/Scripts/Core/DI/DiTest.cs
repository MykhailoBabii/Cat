using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.DI
{


    public class DiTest : MonoBehaviour
    {
        private DiServiceCollection _diServiceCollection;
        private DiContainer _diContainer;

        void Start()
        {
            TestDi();
        }

        private void TestDi()
        {
            _diServiceCollection = new DiServiceCollection();
            //_diServiceCollection.RegisterSingleton(new RandomGuiGenerator());
            //_diServiceCollection.RegisterSingleton<RandomGuiGenerator>();
            _diServiceCollection.RegisterTransient<RandomGuidGenerator>();

            _diServiceCollection.RegisterTransient<ISomeService, SomeServiceOne>();
            _diServiceCollection.RegisterSingleton<IRandomGuidProvider, RandomGuidProvider>();

            _diContainer = _diServiceCollection.GenerateContainer();

            //var servicesFirst = _diContainer.GetService<RandomGuiGenerator>();
            //var servicesSecond = _diContainer.GetService<RandomGuiGenerator>();

            var servicesFirst = _diContainer.GetService<ISomeService>();
            var servicesSecond = _diContainer.GetService<ISomeService>();

            //Debug.Log($"[{GetType().Name}][TestDi] service: {servicesFirst.RandomGuid}");
            //Debug.Log($"[{GetType().Name}][TestDi] service: {servicesSecond.RandomGuid}");
            servicesFirst.PrintSonething();
            servicesSecond.PrintSonething();
        }
    }

    public interface ISomeService
    {
        void PrintSonething();
    }

    

    public class SomeServiceOne : ISomeService
    {
        //private readonly Guid _randomGuid = Guid.NewGuid();
        private readonly IRandomGuidProvider _randomGuidProvider;

        public SomeServiceOne(IRandomGuidProvider randomGuidProvider)
        {
            _randomGuidProvider = randomGuidProvider;
        }

        public void PrintSonething()
        {
            Debug.Log($"[{GetType().Name}][PrintSonething] Guid: {_randomGuidProvider.RandomGuid}");
        }
    }


    public interface IRandomGuidProvider
    {
        Guid RandomGuid { get; }
    }

    public class RandomGuidProvider : IRandomGuidProvider
    {
        public Guid RandomGuid { get; } = System.Guid.NewGuid();
    }

    public class RandomGuidGenerator
    {
        public System.Guid RandomGuid { get; set; } = System.Guid.NewGuid();
    }
}