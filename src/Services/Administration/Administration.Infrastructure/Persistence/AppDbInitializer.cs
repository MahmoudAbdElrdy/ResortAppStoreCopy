using System.Linq;

namespace ResortAppStore.Services.Administration.Infrastructure.Persistence
{
  public partial class AppDbInitializer {
    public AppDbInitializer() {

    }
    public static void Initialize(IdentityServiceDbContext context) {
      var initializer = new AppDbInitializer();
      initializer.SeedAuthEverything(context);
    }    
    
  }

}