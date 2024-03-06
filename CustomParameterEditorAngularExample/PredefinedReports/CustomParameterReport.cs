using DevExpress.XtraReports.Parameters;
using DevExpress.XtraReports.UI;
using System;

namespace CustomParameterEditorAngularExample.PredefinedReports {
    public partial class CustomParameterReport : DevExpress.XtraReports.UI.XtraReport {
        public CustomParameterReport() {
            InitializeComponent();

            // 
            // customMailParameter
            // 
            Parameter customMailParameter = new Parameter {
                Description = "Custom Email Parameter",
                Name = "customMailParameter",
                ValueInfo = "SampleMail@example.com",
                Type = typeof(CustomParameterType),
                Visible = true,
                Enabled = true
            };

            this.Parameters.Add(customMailParameter);

            this.label2.ExpressionBindings.AddRange(new ExpressionBinding[] {
            new ExpressionBinding("BeforePrint", "Text", "?customMailParameter") });
        }
    }
}
