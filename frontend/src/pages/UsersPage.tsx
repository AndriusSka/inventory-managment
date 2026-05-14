import { useEffect, useState } from "react";
import { usersApi } from "../api/usersApi";
import type { User } from "../types/User";

export function UsersPage() {
  const [users, setUsers] = useState<User[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    async function loadUsers() {
      try {
        const data = await usersApi.getAll();
        setUsers(data);
      } catch (err) {
        setError("Įvyko klaida kraunant vartotojų sąrašą.");
        console.error(err);
      } finally {
        setIsLoading(false);
      }
    }
    loadUsers();
  }, []);

  if (isLoading) {
    return <p className="text-slate-500">Kraunama...</p>;
  }

  if (error) {
    return <p className="text-red-600">{error}</p>;
  }

  return (
    <div>
      <h2 className="text-xl font-semibold text-slate-800 mb-4">Vartotojai</h2>

      <div className="bg-white rounded-lg shadow-sm border overflow-hidden">
        <table className="w-full">
          <thead className="bg-slate-100">
            <tr>
              <th className="px-4 py-3 text-left text-sm font-medium text-slate-700">
                Vardas
              </th>
              <th className="px-4 py-3 text-left text-sm font-medium text-slate-700">
                Pavardė
              </th>
              <th className="px-4 py-3 text-left text-sm font-medium text-slate-700">
                ID
              </th>
            </tr>
          </thead>
          <tbody>
            {users.map((user) => (
              <tr key={user.id} className="border-t hover:bg-slate-50">
                <td className="px-4 py-3 text-sm text-slate-800">
                  {user.firstName}
                </td>
                <td className="px-4 py-3 text-sm text-slate-800">
                  {user.lastName}
                </td>
                <td className="px-4 py-3 text-xs text-slate-500 font-mono">
                  {user.id}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}
