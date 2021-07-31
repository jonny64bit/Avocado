# Avocado 🤵
Avocado Meter Reading API

[![test](https://github.com/jonny64bit/Avocado/actions/workflows/test.yml/badge.svg)](https://github.com/jonny64bit/Avocado/actions/workflows/test.yml)

### Response from test file

🤙: `POST => /meter-reading-uploads`

```JSON
{
    "result": "PARTIAL-SUCCESS",
    "detail": {
        "processed": 22,
        "total": 35,
        "errors": [
            "Rejecting duplicate. 2344,22/04/2019 09:24,01002",
            "Rejecting duplicate. 2344,22/04/2019 12:25,01002",
            "Unable to parse meter reading value. 2346,22/04/2019 12:25,999999",
            "Unable to parse meter reading value. 2349,22/04/2019 12:25,VOID",
            "Unrecognized account Id. 2354,22/04/2019 12:25,00889",
            "Unable to parse meter reading value. 2344,08/05/2019 09:24,0X765",
            "Unable to parse meter reading value. 6776,09/05/2019 09:24,-06575",
            "Unable to parse meter reading value. 4534,11/05/2019 09:24,",
            "Unable to parse meter reading value. 1235,13/05/2019 09:24,",
            "Unrecognized account Id. 1236,10/04/2019 19:34,08898",
            "Unrecognized account Id. 1237,15/05/2019 09:24,03455",
            "Unrecognized account Id. 1238,16/05/2019 09:24,00000",
            "Incorrect number of segments. 1241,11/04/2019 09:24,00436,X"
        ]
    }
}
```
