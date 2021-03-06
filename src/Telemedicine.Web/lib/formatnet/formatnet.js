﻿(function () {
    'use strict';

    /**
     * Gets the current locale of browser
     * @returns {string} Current locale name or en-US
     * @ignore
     */
    function getCurrentLocale() {
        return Date.prototype.dictionary.hasOwnProperty(window.navigator.language.toLowerCase()) ? window.navigator.language.toLowerCase() : 'en-us';
    }

    /**
     * Validates format methods' arguments
     * @param {[*]} args - array of method's arguments
     * @param {*} vector - JSON object which contains validation rules
     * @returns {boolean} True if is valid, throws exception otherwise
     * @throws Exception
     * @ignore
     */
    function validateArgs(args, vector) {
        for (var check in vector) {
            if (vector.hasOwnProperty(check)) {
                switch (check) {
                    case 'argsnum':
                        if ((vector[check] == args.length) === false) {
                            throw 'Wrong number of arguments. Valid number of arguments is ' + vector[check];
                        }
                        break;
                    case 'minargsnum':
                        if ((vector[check] <= args.length) === false) {
                            throw 'Too few arguments';
                        }
                        break;
                    case 'maxargsnum':
                        if ((vector[check] >= args.length) === false) {
                            throw 'Too many arguments';
                        }
                        break;
                    case 'regex':
                        if (vector[check].test(args[0]) === false) {
                            throw 'Invalid format';
                        }
                        break;
                }
            }
        }
        return true;
    };

    /**
     * Adds the format method to any String type objects.
     * @example
     * 'Hello, {0}'.format('Nir')
     * // => 'Hello, Nir'
     */
    String.prototype.format = function () {
        var args = arguments;

        return this.replace(/{{|}}|{(\d+)}/g, function (curlyBrack, index) {
            return ((curlyBrack == '{{') ? '{' : ((curlyBrack == '}}') ? '}' : args[index]));
        });
    };

    /**
     * Adds the format method to any Number type objects
     * @example
     * var num = new Number(20.6);
     * num.format('###.00');
     * // => 20.60
     */
    Number.prototype.format = function () {
        var args = arguments,
          clearFormat = null,
          numberArray = null,
          formatArray = null,
          numberInnerArray = null,
          formatInnerArray = null,
          outputArray = [];

        if (validateArgs(args, { argsnum: 1, regex: /[#0\- ]+/ })) {

            //var intDecimalPlaces = Math.max(0, args[0].split('').reverse().indexOf('.'));
            //return this.toFixed(intDecimalPlaces);

            clearFormat = args[0].replace(/[^0#\.]/g, '');
            numberArray = (this.toFixed((clearFormat.length - clearFormat.indexOf('.') - 1) % clearFormat.length).toString()).split('.');
            formatArray = args[0].split('.');
            for (var i = 0; i < formatArray.length; i++) {
                if (i === 0) {
                    numberInnerArray = numberArray[i].split('').reverse(); // Convert string to array
                    formatInnerArray = formatArray[i].indexOf('0') >= 0 ?
                      (formatArray[i].substr(0, formatArray[i].indexOf('0')) + formatArray[i].substr(formatArray[i].indexOf('0')).replace(/#/g, '0')).split('').reverse() : // Convert string to array
                      formatArray[i].split('').reverse();
                }
                else {
                    if (formatArray[i].length > 0) {
                        outputArray.push('.');
                        numberInnerArray = numberArray[i].split(''); // Convert string to array
                        formatInnerArray = formatArray[i].indexOf('0') >= 0 ?
                          (formatArray[i].substr(0, formatArray[i].lastIndexOf('0')) + formatArray[i].substr(formatArray[i].lastIndexOf('0')).replace(/#/g, '0')).split('') : // Convert string to array
                          formatArray[i].split('');
                    }
                }
                while (numberInnerArray.length || formatInnerArray.length) {
                    if (numberInnerArray.length && formatInnerArray.length) {
                        if (/[^0#]/.test(formatInnerArray[0])) {
                            outputArray.push(formatInnerArray.shift());
                        } else {
                            var intLength = (formatInnerArray.join('').match(/[0#]/g).length == 1 ? numberInnerArray.length : 1);
                            for (var j = 0; j < intLength; j++) {
                                outputArray.push(numberInnerArray.shift());
                            }
                            formatInnerArray.shift();
                        }
                    } else if (numberInnerArray.length) {
                        outputArray.push(numberInnerArray.shift());
                    } else {
                        if (/[^#]/.test(formatInnerArray[0])) {
                            outputArray.push(formatInnerArray.shift());
                        }
                        else
                            formatInnerArray.shift();
                    }
                }
                if (i === 0) outputArray.reverse();
            }
            return outputArray.join('');
        }
    };

    /**
     * @property Dictionary object
     * This is the default dictionary. More dictionaries are available under locales folder
     */
    Date.prototype.dictionary = {
        "en-us": {
            Days: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
            DaysShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
            Months: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December']
        }
    };

    /**
     * Adds the format method to any Date type objects.
     * @example
     * var dt = new Date();
     * dt.format('MMM yyyy, hh:mm')
     * // => Oct 2010, 10:00;
     */
    Date.prototype.format = function () {
        var args = arguments,
          format = null,
          temp = null,
          i;

        if (validateArgs(args, { argsnum: 1 })) {
            format = args[0].toString().match(/\w+|[-_/\\\.,: ]+/g);
            for (i = 0; i < format.length; i++) {
                switch (format[i]) {
                    case 'y':     // 15
                        format[i] = parseInt(('0' + this.getFullYear().toString()).substr(-2)).toString();
                        break;
                    case 'yy':    // 15
                        format[i] = ('0' + this.getFullYear().toString()).substr(-2);
                        break;
                    case 'yyy':   // 015
                        format[i] = this.getFullYear().toString().substr(1);
                        break;
                    case 'yyyy':  // 2015
                        format[i] = this.getFullYear();
                        break;
                    case 'yyyyy':  // 02015
                        format[i] = '0' + this.getFullYear();
                        break;
                    case 'M':     // 1
                        format[i] = this.getMonth();
                        break;
                    case 'MM':    // 01
                        format[i] = ('0' + this.getMonth()).substr(-2);
                        break;
                    case 'MMM':   // Jan
                        format[i] = Date.prototype.dictionary[getCurrentLocale()].Months[this.getMonth() - 1].substr(0, 3);
                        break;
                    case 'MMMM':  // January
                        format[i] = Date.prototype.dictionary[getCurrentLocale()].Months[this.getMonth() - 1];
                        break;
                    case 'd':     // 1
                        format[i] = this.getDate();
                        break;
                    case 'dd':    // 01
                        format[i] = ('0' + this.getDate()).substr(-2);
                        break;
                    case 'ddd':   // Sun
                        format[i] = Date.prototype.dictionary[getCurrentLocale()].Days[this.getDay()].substr(0, 3);
                        break;
                    case 'dddd':  // Sunday
                        format[i] = Date.prototype.dictionary[getCurrentLocale()].Days[this.getDay()];
                        break;
                    case 'h':     // 1 (12h)
                        format[i] = this.getHours() % 12;
                        break;
                    case 'hh':    // 01 (12h)
                        format[i] = ('0' + this.getHours() % 12).substr(-2);
                        break;
                    case 'H':     // 1 (24h)
                        format[i] = this.getHours();
                        break;
                    case 'HH':    // 01 (24h)
                        temp = this.getHours();
                        format[i] = ('0' + this.getHours()).substr(-2);
                        break;
                    case 'm':     // 1
                        format[i] = this.getMinutes();
                        break;
                    case 'mm':    // 01
                        format[i] = ('0' + this.getMinutes()).substr(-2);
                        break;
                    case 's':     // 1
                        format[i] = this.getSeconds();
                        break;
                    case 'ss':    // 01
                        format[i] = ('0' + this.getSeconds()).substr(-2);
                        break;
                    case 't':     // A
                        format[i] = (this.getHours() < 12 ? 'A' : 'P');
                        break;
                    case 'tt':    // AM
                        format[i] = (this.getHours() < 12 ? 'AM' : 'PM');
                        break;
                    case 'z':     // +4
                        temp = 6 + this.getTimezoneOffset() / 60;
                        format[i] = (temp > 0 ? '+' : '') + temp;
                        break;
                    case 'zz':    // +04
                        temp = 6 + this.getTimezoneOffset() / 60;
                        format[i] = (temp > 0 ? '+' : '') + ('0' + temp).substr(-2);
                        break;
                    case 'zzz':    // +04:00
                        temp = 6 + this.getTimezoneOffset() / 60;
                        format[i] = (temp > 0 ? '+' : '') + ('0' + temp).substr(-2) + ':' + '00';
                        break;
                    case 'shortTime':
                        format[i] = this.format('h:mm tt');
                        break;
                    case 'shortDate':
                        format[i] = this.format('M/d/yyyy');
                        break;
                    case 'longTime':
                        format[i] = this.format('h:mm:ss tt');
                        break;
                    case 'longDate':
                        format[i] = this.format('dddd, MMMM dd, yyyy');
                        break;
                    case 'fullDateTime':
                        format[i] = this.format('dddd, MMMM dd, yyyy h:mm:ss tt');
                        break;
                    case 'sortableDateTime':
                        format[i] = this.format('yyyy-MM-dd~HH:mm:ss').replace('~', 'T');
                        break;
                }
            }
            return (format.join(''));
        }
    };
})();