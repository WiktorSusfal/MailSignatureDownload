MailSignatureDownload
Aplikacja WPF C# do pobierania i przeglądania podpisu maila z OWA

Funkcja do pobierania podpisu poczty (public void GetSignature) znajduje się w klasie ViewModel.

Podpis poczty jest pobierany w formacie HTML i następnie wyświetlany w kontrolce WPF WebBrowser.

Póki co poświadczenia do logowania są przechowywane wprost w kodzie programu. Ulepszenie pod tym kątem można uzyskać poprzez:
- każdorazowe zapytanie o hasło użytkownika przed podłączeniem do skrzynki
- przechowywanie hasła w formie zaszyfrowanej np. z wykorzystaniem Windows Data Protection API (DPAPI).

Wydajność (ilość zapytań i odpowiedzi w czasie) jest ograniczona, gdyż za każdym wywołaniem komendy tworzony jest nowy obiekt typu 'ExchangeService' wraz z funckją 'AutodiscoverUrl', której wykonanie zajmuje więcej czasu. Obiekt typu ExchangeService mógłby być osobną właściwością w głównej klasie ViewModel, a jego ponowna inicjalizacja mogłaby następować tylko przy zmianie parametrów zadania. 

Do obsłgu błedów wykorzystuję klasę 'RelayCommand' - przechowującą delegaty dla 2 funckji: wykonującej określone działanie w systemie oraz sprawdzającej możliwość użycia tej pierwszej. Klasa implementuje interfejs 'ICommand', gdzie w funckji 'Execute' następuje wywołanie delegatu z główną funkcją w klauzuli try-catch - pełniącej rolę wspólnej obsługi błędów dla wszystkich komend wywoływanych z poziomu interfejsu. Obsługę błędów można ulepszyć poprzez dodanie większej liczby klauzul 'catch' oraz zróżnicowanie zachowania programu względem tego, która z nich wywoła się przy danym wyjątku.
