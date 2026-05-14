import { useState } from "react";
import { UsersPage } from "./pages/UsersPage";
import { InventoryItemsPage } from "./pages/InventoryItemsPage";

type TabKey = "users" | "inventory";

function App() {
  const [activeTab, setActiveTab] = useState<TabKey>("inventory");

  return (
    <div className="min-h-screen bg-slate-50">
      <header className="bg-white shadow-sm border-b">
        <div className="max-w-7xl mx-auto px-4">
          <div className="py-4">
            <h1 className="text-2xl font-bold text-slate-800">
              Inventoriaus valdymas
            </h1>
          </div>
          <nav className="flex gap-1 -mb-px">
            <TabButton
              isActive={activeTab === "inventory"}
              onClick={() => setActiveTab("inventory")}
            >
              Inventorius
            </TabButton>
            <TabButton
              isActive={activeTab === "users"}
              onClick={() => setActiveTab("users")}
            >
              Vartotojai
            </TabButton>
          </nav>
        </div>
      </header>

      <main className="max-w-7xl mx-auto px-4 py-8">
        {activeTab === "inventory" && <InventoryItemsPage />}
        {activeTab === "users" && <UsersPage />}
      </main>
    </div>
  );
}

interface TabButtonProps {
  isActive: boolean;
  onClick: () => void;
  children: React.ReactNode;
}

function TabButton({ isActive, onClick, children }: TabButtonProps) {
  const baseClasses = "px-4 py-2 text-sm font-medium border-b-2 transition";
  const activeClasses = "border-blue-600 text-blue-600";
  const inactiveClasses =
    "border-transparent text-slate-600 hover:text-slate-800";

  return (
    <button
      onClick={onClick}
      className={`${baseClasses} ${isActive ? activeClasses : inactiveClasses}`}
    >
      {children}
    </button>
  );
}

export default App;
