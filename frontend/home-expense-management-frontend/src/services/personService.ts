import { api } from "./api";
import { type Person } from "../types";

interface PersonModel {
  name: string;
  age: number;
}

interface PersonCreatedResponse {
  id: string;
}

const mapPerson = (raw: any): Person => ({
  id: raw.id,
  name: raw.name,
  age: raw.age,
});

export const personService = {
  getAll: async (): Promise<Person[]> => {
    const data = await api<any[]>("/api/persons");
    return data.map(mapPerson);
  },

  getById: async (id: string): Promise<Person> => {
    const data = await api<any>(`/api/persons/${id}`);
    return mapPerson(data);
  },

  create: async (person: Omit<Person, "id">): Promise<Person> => {
    const body: PersonModel = {
      name: person.name,
      age: person.age,
    };

    const response = await api<PersonCreatedResponse>("/api/persons", {
      method: "POST",
      body: JSON.stringify(body),
    });

    return personService.getById(response.id);
  },

  update: async (id: string, person: Partial<Person>): Promise<Person> => {
    const current = await personService.getById(id);

    const body: PersonModel = {
      name: person.name ?? current.name,
      age: person.age ?? current.age,
    };

    await api<void>(`/api/persons/${id}`, {
      method: "PUT",
      body: JSON.stringify(body),
    });

    return { ...current, ...person };
  },

  delete: (id: string): Promise<void> => {
    return api<void>(`/api/persons/${id}`, {
      method: "DELETE",
    });
  },
};