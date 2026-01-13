using GestureCommandEngine.Core.Models;

namespace GestureCommandEngine.Core.Interfaces;

public interface ICommandsRepository
{
    bool Has(string commandId);
    bool Has(Command command);
    Command? Get(string commandId);
    void Add(Command command);
    void Add(string commandId, string description);
    void Update(string commandId, string description);
    void Remove(string commandId);
    void Remove(Command command);
}
