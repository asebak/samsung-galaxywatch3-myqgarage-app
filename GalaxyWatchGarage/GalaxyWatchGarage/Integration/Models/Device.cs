using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamsungWatchGarage.Integration.Models
{
    public class DeviceResponse
    {
        [JsonProperty("href")]
        public string Href { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("items")]
        public List<Device> Items { get; set; }
    }
    public class Device
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }

        [JsonProperty("device_family")]
        public string DeviceFamily { get; set; }

        [JsonProperty("device_platform")]
        public string DevicePlatform { get; set; }

        [JsonProperty("device_type")]
        public string DeviceType { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("parent_device")]
        public string ParentDevice { get; set; }

        [JsonProperty("parent_device_id")]
        public string ParentDeviceId { get; set; }

        [JsonProperty("created_date")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("state")]
        public State State { get; set; }
    }

    public class State
    {
        [JsonProperty("gdo_lock_connected")]
        public bool GdoLockConnected { get; set; }

        [JsonProperty("attached_work_light_error_present")]
        public bool AttachedWorkLightErrorPresent { get; set; }

        [JsonProperty("door_state")]
        public string DoorState { get; set; }

        [JsonProperty("open")]
        public string Open { get; set; }

        [JsonProperty("close")]
        public string Close { get; set; }

        [JsonProperty("last_update")]
        public DateTime LastUpdate { get; set; }

        [JsonProperty("passthrough_interval")]
        public string PassthroughInterval { get; set; }

        [JsonProperty("door_ajar_interval")]
        public string DoorAjarInterval { get; set; }

        [JsonProperty("invalid_credential_window")]
        public string InvalidCredentialWindow { get; set; }

        [JsonProperty("invalid_shutout_period")]
        public string InvalidShutoutPeriod { get; set; }

        [JsonProperty("is_unattended_open_allowed")]
        public bool IsUnattendedOpenAllowed { get; set; }

        [JsonProperty("is_unattended_close_allowed")]
        public bool IsUnattendedCloseAllowed { get; set; }

        [JsonProperty("aux_relay_delay")]
        public string AuxRelayDelay { get; set; }

        [JsonProperty("use_aux_relay")]
        public bool UseAuxRelay { get; set; }

        [JsonProperty("aux_relay_behavior")]
        public string AuxRelayBehavior { get; set; }

        [JsonProperty("rex_fires_door")]
        public bool RexFiresDoor { get; set; }

        [JsonProperty("command_channel_report_status")]
        public bool CommandChannelReportStatus { get; set; }

        [JsonProperty("control_from_browser")]
        public bool ControlFromBrowser { get; set; }

        [JsonProperty("report_forced")]
        public bool ReportForced { get; set; }

        [JsonProperty("report_ajar")]
        public bool ReportAjar { get; set; }

        [JsonProperty("max_invalid_attempts")]
        public int MaxInvalidAttempts { get; set; }

        [JsonProperty("online")]
        public bool Online { get; set; }

        [JsonProperty("last_status")]
        public DateTime LastStatus { get; set; }
    }
}
