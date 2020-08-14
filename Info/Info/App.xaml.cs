using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DLToolkit.Forms.Controls;

namespace Info
{
    public partial class App : Application
    {
        public const string DBFILENAME = "Zavod.db";
        public const string DATABASE_NAME = "Zavod5.db";
        public static InstrumentAsyncRepository database;
        public static InstrumentAsyncRepository Database
        {
            get
            {
                if (database == null)
                {
                    // путь, по которому будет находиться база данных
                    string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DATABASE_NAME);
                    // если база данных не существует (еще не скопирована)
                    if (!File.Exists(dbPath))
                    {
                        // получаем текущую сборку
                        var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                        // берем из нее ресурс базы данных и создаем из него поток
                        using (Stream stream = assembly.GetManifestResourceStream($"Info.{DATABASE_NAME}"))
                        {
                            using (FileStream fs = new FileStream(dbPath, FileMode.OpenOrCreate))
                            {
                                stream.CopyTo(fs);  // копируем файл базы данных в нужное нам место
                                fs.Flush();
                            }
                        }
                    }
                    database = new InstrumentAsyncRepository(dbPath);
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            FlowListView.Init();
            string dbPathtemp = DependencyService.Get<IPath>().GetDatabasePath(DBFILENAME);
            using (var db = new ZavodDB(dbPathtemp))
            {
                db.Database.EnsureCreated();
                if (db.Instruments.Count() < 5)
                {
                    var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
                    Stream stream = assembly.GetManifestResourceStream("Info.Instruments.xml");
                    ObservableCollection<Instruments> lstInst;
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        var serializer = new XmlSerializer(typeof(ObservableCollection<Instruments>));
                        lstInst = (ObservableCollection<Instruments>)serializer.Deserialize(reader);
                    }
                    foreach (var item in lstInst)
                    {
                        db.Instruments.Add(item);
                    }
                    db.SaveChanges();
                }
            }
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
