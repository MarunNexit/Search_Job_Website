// This file can be replaced during build by using the `fileReplacements` array.
// `ng build` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.

export const environment = {
  production: false,
  apiUrl: "https://localhost:7019/api",
  apiUrl2: "https://localhost:7019",
  sas: 'sp=racwdli&st=2024-06-06T08:56:06Z&se=2024-07-06T16:56:06Z&spr=https&sv=2022-11-02&sr=c&sig=VwBhdgNTOT3LGkgJxW5nQfsif8UMSHOwZi8MbSfDZkY%3D',
  AZURE_STORAGE_CONNECTION_STRING: "DefaultEndpointsProtocol=https;AccountName=websearchjob;AccountKey=xcfgGMcq6q3Upi5Da5E0jqlhaZxd5Zh+eTm4o+CsElsHic6lMWCLFR6BzdA/cx5d97wwXa/t7lmh+AStRU+sCQ==;EndpointSuffix=core.windows.net",
  socialLogin: {
    google: {
      clientId:
        '522560406608-dv0sdn5hdihukmnqbm3ekhjbnh0cojiq.apps.googleusercontent.com',
    },
    facebook: {
      clientId: ' ------ ',
    },
  },
};


/*apiUrl: "https://searchjobbloomapi.azurewebsites.net/api",
  apiUrl2: "https://searchjobbloomapi.azurewebsites.net",*/

/*
 * For easier debugging in development mode, you can import the following file
 * to ignore zone related error stack frames such as `zone.run`, `zoneDelegate.invokeTask`.
 *
 * This import should be commented out in production mode because it will have a negative impact
 * on performance if an error is thrown.
 */
// import 'zone.js/plugins/zone-error';  // Included with Angular CLI.
