module Telemedicine {
    export class RecommendationListController {
        constructor(private recommendationApiService: RecommendationApiService,
            private $routeParams: ItemRouteParams) { }
    }
}