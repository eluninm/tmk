module Telemedicine {
    export function ToDate() {
        return function (input) { 
            return new Date(input);
        }
    }
}