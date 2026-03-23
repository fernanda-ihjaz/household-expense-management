import { useEffect } from "react";
import { useAppContext } from "../context/AppContext";
import type { Person } from "../types";
import { personService } from "../services/personService";

export const usePeople = () => {
  const { people, setPeople, setTransactions } = useAppContext();

  useEffect(() => {
    const loadPeople = async () => {
      try {
        const data = await personService.getAll();
        setPeople(data);
      } catch (error) {
        console.error("Erro ao carregar pessoas", error);
      }
    };

    loadPeople();
  }, []);

  const addPerson = async (data: Omit<Person, "id">) => {
    try {
      const newPerson = await personService.create(data);
      setPeople((prev) => [...prev, newPerson]);
    } catch (error) {
      console.error("Erro ao criar pessoa", error);
    }
  };

  const editPerson = async (id: string, data: Partial<Person>) => {
    try {
      const updated = await personService.update(id, data);
      setPeople((prev) =>
        prev.map((p) => (p.id === id ? updated : p))
      );
    } catch (error) {
      console.error("Erro ao editar pessoa", error);
    }
  };

  const deletePerson = async (id: string) => {
    try {
      await personService.delete(id);

      setPeople((prev) => prev.filter((p) => p.id !== id));
      setTransactions((prev) => prev.filter((t) => t.personId !== id));
    } catch (error) {
      console.error("Erro ao deletar pessoa", error);
    }
  };

  return {
    people,
    addPerson,
    editPerson,
    deletePerson,
  };
};