# Speed Test GetFlashcardsListWithProgressesH vs GetFlashcardsListWithProgressesParallelH

This test show how much more parallel approach is better for retreiving list of flashcards with extra data (For both native to foreign and foreign to native: PracticeDate, CorrectAnsInRow and next practice date if user answer correctly.)

## Tests results

This table shows how much time handler needs to prepare data.
* SYNC - GetFlashcardsListWithProgressesH
* PARALLEL - GetFlashcardsListWithProgressesParallelH

| QTY | SYNC | PARALLEL |
| :---: | ---: | ---: |
| 10 | 18 | 17 |
| 50 | 71 | 70 |
| 100 | 135 | 125 |
| 300 | 38 | 35 |
| 500 | 67 | 49 |
| 1000 | 145 | 108 |
| 2000 | 397 | 235 |
| 5000 | 1856 | 933 |
| 10000 | 6958 | 3541 |

(time in miliseconds)

## Conclusions

Results shows that PARALLEL approach is better even with few flashcards in collection.
In range of 10-300 flaschcards in collection there is small difference between time handlers needs to execute data.
This difference become significant when number of flashcards in collection increases.