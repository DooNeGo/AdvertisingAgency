using AdvertisingAgency.Domain;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace AdvertisingAgency.Messages;

public sealed class AddCampaignMessage(CampaignId value) : ValueChangedMessage<CampaignId>(value);