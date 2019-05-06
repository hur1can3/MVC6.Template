numbro.registerLanguage({
    languageTag: "lt",
    delimiters: {
        thousands: ".",
        decimal: ","
    },
    abbreviations: {
        thousand: " tūkst.",
        million: " mln.",
        billion: " mlrd.",
        trillion: " trln."
    },
    ordinal: function() {
        return "";
    },
    currency: {
        symbol: "€",
        position: "postfix",
        code: "EUR"
    },
    currencyFormat: {
        thousandSeparated: true,
        totalLength: 4,
        spaceSeparated: true,
        average: true
    },
    formats: {
        fourDigits: {
            totalLength: 4,
            spaceSeparated: true,
            average: true
        },
        fullWithTwoDecimals: {
            output: "currency",
            thousandSeparated: true,
            spaceSeparated: true,
            mantissa: 2
        },
        fullWithTwoDecimalsNoCurrency: {
            mantissa: 2,
            thousandSeparated: true
        },
        fullWithNoDecimals: {
            output: "currency",
            thousandSeparated: true,
            spaceSeparated: true,
            mantissa: 0
        }
    }
});
