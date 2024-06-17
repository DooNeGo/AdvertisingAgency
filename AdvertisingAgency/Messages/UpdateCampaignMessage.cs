using AdvertisingAgency.Domain;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdvertisingAgency.Messages;

public sealed class UpdateCampaignMessage(CampaignId value) : ValueChangedMessage<CampaignId>(value);
