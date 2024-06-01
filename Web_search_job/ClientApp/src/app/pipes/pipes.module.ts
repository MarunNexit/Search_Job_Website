import {NgModule} from "@angular/core";

import {TruncatePipe} from "./truncate.pipe";
import {SafeHtmlPipe} from "./safe-html.pipe";
import {CurrencySymbolPipe} from "./currency-symbol.pipe";

@NgModule({
  declarations: [
    TruncatePipe,
    SafeHtmlPipe,
    CurrencySymbolPipe,
  ],
  imports: [

  ],
  exports: [
    TruncatePipe,
    SafeHtmlPipe,
    CurrencySymbolPipe,
  ]
})
export class PipeModule { }
