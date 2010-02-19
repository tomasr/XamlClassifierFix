using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace Winterdom.VisualStudio.Extensions.Text {
   [Export(typeof(IWpfTextViewCreationListener))]
   [ContentType("XAML")]
   [Name("XAML Fix")]
   [TextViewRole(PredefinedTextViewRoles.Document)]
   public class XamlFixCreationListener : IWpfTextViewCreationListener {
      [Import]
      IClassificationTypeRegistryService registry = null;
      [Import]
      IClassificationFormatMapService mapService = null;

      public void TextViewCreated(IWpfTextView textView) {
         var formatMap = mapService.GetClassificationFormatMap(textView);
         var textClassification = registry.GetClassificationType("text");
         var stringClassification = registry.GetClassificationType("string");
         var props = formatMap.GetExplicitTextProperties(textClassification);
         formatMap.SetExplicitTextProperties(stringClassification, props);
      }
   }
}
