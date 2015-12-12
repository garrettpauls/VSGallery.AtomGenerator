﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.6.1055.0.
// 
namespace VSGallery.AtomGenerator.Vsix.Schemas {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schemas.microsoft.com/developer/vsx-schema/2011")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://schemas.microsoft.com/developer/vsx-schema/2011", IsNullable=false)]
    public partial class PackageManifest {
        
        private Metadata metadataField;
        
        private Installation installationField;
        
        private Dependency[] dependenciesField;
        
        private Asset[] assetsField;
        
        private System.Xml.XmlElement[] anyField;
        
        private string versionField;
        
        /// <remarks/>
        public Metadata Metadata {
            get {
                return this.metadataField;
            }
            set {
                this.metadataField = value;
            }
        }
        
        /// <remarks/>
        public Installation Installation {
            get {
                return this.installationField;
            }
            set {
                this.installationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Dependency[] Dependencies {
            get {
                return this.dependenciesField;
            }
            set {
                this.dependenciesField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable=false)]
        public Asset[] Assets {
            get {
                return this.assetsField;
            }
            set {
                this.assetsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/developer/vsx-schema/2011")]
    public partial class Metadata {
        
        private MetadataIdentity identityField;
        
        private string displayNameField;
        
        private string descriptionField;
        
        private string moreInfoField;
        
        private string licenseField;
        
        private string gettingStartedGuideField;
        
        private string releaseNotesField;
        
        private string iconField;
        
        private string previewImageField;
        
        private string tagsField;
        
        private System.Xml.XmlElement[] anyField;
        
        /// <remarks/>
        public MetadataIdentity Identity {
            get {
                return this.identityField;
            }
            set {
                this.identityField = value;
            }
        }
        
        /// <remarks/>
        public string DisplayName {
            get {
                return this.displayNameField;
            }
            set {
                this.displayNameField = value;
            }
        }
        
        /// <remarks/>
        public string Description {
            get {
                return this.descriptionField;
            }
            set {
                this.descriptionField = value;
            }
        }
        
        /// <remarks/>
        public string MoreInfo {
            get {
                return this.moreInfoField;
            }
            set {
                this.moreInfoField = value;
            }
        }
        
        /// <remarks/>
        public string License {
            get {
                return this.licenseField;
            }
            set {
                this.licenseField = value;
            }
        }
        
        /// <remarks/>
        public string GettingStartedGuide {
            get {
                return this.gettingStartedGuideField;
            }
            set {
                this.gettingStartedGuideField = value;
            }
        }
        
        /// <remarks/>
        public string ReleaseNotes {
            get {
                return this.releaseNotesField;
            }
            set {
                this.releaseNotesField = value;
            }
        }
        
        /// <remarks/>
        public string Icon {
            get {
                return this.iconField;
            }
            set {
                this.iconField = value;
            }
        }
        
        /// <remarks/>
        public string PreviewImage {
            get {
                return this.previewImageField;
            }
            set {
                this.previewImageField = value;
            }
        }
        
        /// <remarks/>
        public string Tags {
            get {
                return this.tagsField;
            }
            set {
                this.tagsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://schemas.microsoft.com/developer/vsx-schema/2011")]
    public partial class MetadataIdentity {
        
        private string idField;
        
        private string versionField;
        
        private string languageField;
        
        private string publisherField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Language {
            get {
                return this.languageField;
            }
            set {
                this.languageField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Publisher {
            get {
                return this.publisherField;
            }
            set {
                this.publisherField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/developer/vsx-schema/2011")]
    public partial class Asset {
        
        private System.Xml.XmlElement[] anyField;
        
        private string typeField;
        
        private string pathField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyElementAttribute()]
        public System.Xml.XmlElement[] Any {
            get {
                return this.anyField;
            }
            set {
                this.anyField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Path {
            get {
                return this.pathField;
            }
            set {
                this.pathField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Dependency))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/developer/vsx-schema/2011")]
    public partial class DependencyInfo {
        
        private string idField;
        
        private string displayNameField;
        
        private string locationField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string DisplayName {
            get {
                return this.displayNameField;
            }
            set {
                this.displayNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Location {
            get {
                return this.locationField;
            }
            set {
                this.locationField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/developer/vsx-schema/2011")]
    public partial class Dependency : DependencyInfo {
        
        private string versionField;
        
        private bool canAutoDownloadField;
        
        private bool isRequiredField;
        
        public Dependency() {
            this.canAutoDownloadField = false;
            this.isRequiredField = true;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(false)]
        public bool CanAutoDownload {
            get {
                return this.canAutoDownloadField;
            }
            set {
                this.canAutoDownloadField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool IsRequired {
            get {
                return this.isRequiredField;
            }
            set {
                this.isRequiredField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/developer/vsx-schema/2011")]
    public partial class InstallationTarget {
        
        private string idField;
        
        private string versionField;
        
        private string targetPlatformIdentifierField;
        
        private string targetPlatformVersionField;
        
        private string sdkNameField;
        
        private string sdkVersionField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TargetPlatformIdentifier {
            get {
                return this.targetPlatformIdentifierField;
            }
            set {
                this.targetPlatformIdentifierField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string TargetPlatformVersion {
            get {
                return this.targetPlatformVersionField;
            }
            set {
                this.targetPlatformVersionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SdkName {
            get {
                return this.sdkNameField;
            }
            set {
                this.sdkNameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string SdkVersion {
            get {
                return this.sdkVersionField;
            }
            set {
                this.sdkVersionField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://schemas.microsoft.com/developer/vsx-schema/2011")]
    public partial class Installation {
        
        private InstallationTarget[] installationTargetField;
        
        private bool installedByMsiField;
        
        private bool installedByMsiFieldSpecified;
        
        private bool systemComponentField;
        
        private bool systemComponentFieldSpecified;
        
        private bool allUsersField;
        
        private bool allUsersFieldSpecified;
        
        private bool experimentalField;
        
        private bool experimentalFieldSpecified;
        
        private string scopeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("InstallationTarget")]
        public InstallationTarget[] InstallationTarget {
            get {
                return this.installationTargetField;
            }
            set {
                this.installationTargetField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool InstalledByMsi {
            get {
                return this.installedByMsiField;
            }
            set {
                this.installedByMsiField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool InstalledByMsiSpecified {
            get {
                return this.installedByMsiFieldSpecified;
            }
            set {
                this.installedByMsiFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool SystemComponent {
            get {
                return this.systemComponentField;
            }
            set {
                this.systemComponentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SystemComponentSpecified {
            get {
                return this.systemComponentFieldSpecified;
            }
            set {
                this.systemComponentFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool AllUsers {
            get {
                return this.allUsersField;
            }
            set {
                this.allUsersField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AllUsersSpecified {
            get {
                return this.allUsersFieldSpecified;
            }
            set {
                this.allUsersFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public bool Experimental {
            get {
                return this.experimentalField;
            }
            set {
                this.experimentalField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ExperimentalSpecified {
            get {
                return this.experimentalFieldSpecified;
            }
            set {
                this.experimentalFieldSpecified = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Scope {
            get {
                return this.scopeField;
            }
            set {
                this.scopeField = value;
            }
        }
    }
}
