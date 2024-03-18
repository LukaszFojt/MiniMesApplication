using Microsoft.AspNetCore.Mvc;
using System.Reflection.PortableExecutable;

/**
 * Interfejs reprezentujący maszynę.
 */
public interface IMachineParameter
{
    int Id { get; set; }
    DateTime DateTime { get; set; }
    int MachineId { get; set; }
    int ParameterId { get; set; }
}

/**
 * Interfejs repozytorium maszyn.
 */
public interface IMachineParameterRepository
{
    /**
     * Metoda asynchroniczna pobierająca listę wszystkich maszyn.
     * @return Lista maszyn.
     */
    //Task<List<IMachineParameter>> GetAllMachinesParameter();
    Task<List<IMachineParameter>> GetParametersByMachineId(int machineId);

    /**
     * Metoda asynchroniczna dodająca nową maszynę.
     * @param machine Obiekt reprezentujący nową maszynę.
     */
    Task AddMachineParameter(IMachineParameter machineParameter);

    /**
     * Metoda asynchroniczna usuwająca maszynę o podanym identyfikatorze.
     * @param id Identyfikator maszyny do usunięcia.
     */
    Task DeleteMachineParameter(int id);

    /**
     * Metoda asynchroniczna aktualizująca dane maszyny.
     * @param id Identyfikator maszyny do zaktualizowania.
     * @param updatedMachineData Nowe dane maszyny.
     */
    Task UpdateMachineParameter(int id, IMachineParameter updatedMachineParameterData);
}

/*  Wdrożenie wielu skryptów do obsługi danych w bazie danych, takich jak MachineRepository, MachineInterface i MachineController, 
 *  wynika z praktyk dobrej organizacji kodu oraz separacji odpowiedzialności w aplikacji. 
 *  Poniżej wyjaśniam dlaczego każdy z tych skryptów jest potrzebny:

    Machine Interface(IMachine):
    Interfejs IMachine definiuje strukturę danych dla maszyny. Jest to rodzaj kontraktu, który określa, jakie właściwości powinna zawierać każda maszyna w systemie.
    Używanie interfejsu umożliwia elastyczność, ponieważ pozwala na tworzenie różnych implementacji maszyn, które jednak muszą zgodnie z kontraktem zawierać te same właściwości.
    Interfejsy są również przydatne do tworzenia luźnych powiązań między komponentami systemu.

    Machine Repository (MachineRepository):
    Repozytorium maszyny MachineRepository odpowiada za bezpośrednie komunikowanie się z bazą danych w celu wykonywania operacji CRUD (Create, Read, Update, Delete) na maszynach.
    Oddzielenie logiki dostępu do danych w repozytorium pozwala na izolację kodu odpowiedzialnego za operacje na bazie danych.
    Dzięki temu zmiany w logice dostępu do danych nie wpływają bezpośrednio na pozostałe części aplikacji.

    Machine Controller (MachineController):
    Kontroler maszyny MachineController jest odpowiedzialny za obsługę żądań HTTP związanych z maszynami, takich jak pobieranie, dodawanie, aktualizowanie i usuwanie maszyn.
    Kontroler działa jako interfejs komunikacyjny między klientem (np. przeglądarką internetową, aplikacją mobilną) a serwerem.
    Interpretuje żądania klienta i deleguje odpowiednie operacje do repozytorium.

    Kombinacja tych skryptów tworzy architekturę opartą na wzorcu MVC (Model-View-Controller) lub wzorcu podobnym, co jest powszechnie stosowane w tworzeniu aplikacji internetowych.
    Jest to popularny sposób organizacji kodu, który zapewnia czytelność, łatwość utrzymania i skalowalność aplikacji poprzez podział na trzy warstwy: 
    - warstwę prezentacji(kontrolery), 
    - warstwę logiki biznesowej (repozytoria)
    - warstwę dostępu do danych (interfejsy).
*/