﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Tavisca.Content.ImageQuality.Model.Models.Errors {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class FaultMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal FaultMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Tavisca.Content.ImageQuality.Model.Models.Errors.FaultMessages", typeof(FaultMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Memory Stream was not created as the buffer was null. .
        /// </summary>
        public static string BufferNullToCreateMemoryStream {
            get {
                return ResourceManager.GetString("BufferNullToCreateMemoryStream", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Download of file failed.
        /// </summary>
        public static string DownloadFailure {
            get {
                return ResourceManager.GetString("DownloadFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image file creation failed.
        /// </summary>
        public static string FileCreationFailure {
            get {
                return ResourceManager.GetString("FileCreationFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to File is currently in use..
        /// </summary>
        public static string FileInUseError {
            get {
                return ResourceManager.GetString("FileInUseError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid value provided for {0} in the request..
        /// </summary>
        public static string InvalidFieldValue {
            get {
                return ResourceManager.GetString("InvalidFieldValue", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid values provided for the following request headers: {0}..
        /// </summary>
        public static string InvalidRequestHeaders {
            get {
                return ResourceManager.GetString("InvalidRequestHeaders", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IO Error while reading stream.
        /// </summary>
        public static string IOErrorWhileReadingStream {
            get {
                return ResourceManager.GetString("IOErrorWhileReadingStream", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value for the {0} field is required in the request..
        /// </summary>
        public static string MissingField {
            get {
                return ResourceManager.GetString("MissingField", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The following request headers are required: {0}..
        /// </summary>
        public static string MissingRequestHeaders {
            get {
                return ResourceManager.GetString("MissingRequestHeaders", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The image has no featurs for quality detection.
        /// </summary>
        public static string NoBrisqueFeatures {
            get {
                return ResourceManager.GetString("NoBrisqueFeatures", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No Urls were sent for cheking quality.
        /// </summary>
        public static string NoUrl {
            get {
                return ResourceManager.GetString("NoUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Null or empty image url.
        /// </summary>
        public static string NullOrEmptyImageUrl {
            get {
                return ResourceManager.GetString("NullOrEmptyImageUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Memory out of bounds while reading stream.
        /// </summary>
        public static string OutOfMemoryWhileReadingStream {
            get {
                return ResourceManager.GetString("OutOfMemoryWhileReadingStream", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to stream reading failure.
        /// </summary>
        public static string StreamReadingFailure {
            get {
                return ResourceManager.GetString("StreamReadingFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An unexpected error has occured.
        /// </summary>
        public static string UnexpectedSystemException {
            get {
                return ResourceManager.GetString("UnexpectedSystemException", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Url download failure.
        /// </summary>
        public static string UrlDownloadFailure {
            get {
                return ResourceManager.GetString("UrlDownloadFailure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The following validation errors occurred:.
        /// </summary>
        public static string ValidationFailure {
            get {
                return ResourceManager.GetString("ValidationFailure", resourceCulture);
            }
        }
    }
}
