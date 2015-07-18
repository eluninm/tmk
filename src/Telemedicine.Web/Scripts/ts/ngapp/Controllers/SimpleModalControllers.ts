///<reference path="../common/ModalControllerBase.ts"/>

module Telemedicine {
    export class RecommendationDetailsController extends ItemModalViewModel<IRecommendation> {
        constructor($modalInstance: ng.ui.bootstrap.IModalServiceInstance, item: IRecommendation) {
            super($modalInstance, item);
        }
    }
}