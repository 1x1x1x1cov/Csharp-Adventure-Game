using System;
using System.Collections.Generic;

namespace SwinAdventure
{
    public class IdentifiableObject
    {
        private List<string> _identifiers;

        public string FirstId
        {
            get
            {
                return _identifiers.Count > 0 ? _identifiers[0] : "";
            }
        }

        public IdentifiableObject(string[] idents)
        {
            _identifiers = new List<string>();
            foreach (string ident in idents)
            {
                AddIdentifier(ident);
            }
        }

        public void AddIdentifier(string id)
        {
            if (!_identifiers.Contains(id, StringComparer.OrdinalIgnoreCase))
            {
                _identifiers.Add(id);
            }
        }

        public bool AreYou(string id)
        {
            return _identifiers.Contains(id, StringComparer.OrdinalIgnoreCase);
        }

        public void RemoveIdentifier(string id)
        {
            _identifiers.RemoveAll(s => string.Equals(s, id, StringComparison.OrdinalIgnoreCase));
        }

        public void ReplaceFirstIdentifier(string newId)
        {
            if (_identifiers.Count > 0)
            {
                _identifiers[0] = newId;
            }
        }

        public void PrivilegeEscalation(string pin)
        {
            if (pin == "0710")
            {
                ReplaceFirstIdentifier("NewTutorialID");
            }
        }
    }
} 